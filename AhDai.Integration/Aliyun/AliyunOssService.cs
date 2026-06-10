using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using AhDai.Integration.Aliyun.Providers;
using AhDai.Integration.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunOssService
/// </summary>
[Attributes.Service()]
internal class AliyunOssService(IAliyunOssConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AliyunOssConfig, IAliyunOssConfigProvider>(configProvider, httpClientFactory)
    , IAliyunOssService
{
    const string SignatureVersion = "OSS4-HMAC-SHA256";
    const string UnsignedPayload = "UNSIGNED-PAYLOAD";

    readonly FileExtensionContentTypeProvider ContentTypeProvider = new();

    protected override string ServiceName => "阿里云对象存储";


    protected string GetContentType(string extension)
    {
        ContentTypeProvider.TryGetContentType(extension, out var contentType);
        return contentType ?? "application/octet-stream";
    }

    public async Task<OssPolicyTokenOutput> GeneratePolicyTokenAsync(OssPolicyTokenInput input)
    {
        var config = await GetConfigAsync();
        var policyConfig = new OssPolicyConfig()
        {
            Expiration = input.Expiration.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"),
            Conditions = []
        };
        policyConfig.Conditions.Add(["content-length-range", 0, input.MaxLength]);
        policyConfig.Conditions.Add(["starts-with", "$key", input.KeyPrefix]);
        if (input.ContentTypes != null && input.ContentTypes.Length > 0)
        {
            policyConfig.Conditions.Add(["in", "content-type", input.ContentTypes]);
        }

        var callback = new Dictionary<string, object>()
        {
            ["callbackUrl"] = input.CallbackUrl,
            //["callbackHost"]= "",
            ["callbackBody"] = $"{{\"region\":\"{input.Region}\",\"bucket\":${{bucket}},\"name\":\"{input.Name}\",\"object\":${{object}},\"mimeType\":${{mimeType}},\"length\":${{size}},\"hash\":\"{input.Hash}\",\"width\":${{imageInfo.width}},\"height\":${{imageInfo.height}},\"test\":1}}",
            ["callbackSNI"] = true,
            ["callbackBodyType"] = "application/json",
        };
        var callbackBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtil.Serialize(callback)));

        var policy = JsonUtil.Serialize(policyConfig);
        var policyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(policy));
        using var sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(config.AccessKeySecret));
        var signature = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(policyBase64)));

        var host = AliyunOssHelper.GetHost(input.Region, input.Bucket);
        return new OssPolicyTokenOutput()
        {
            AccessKeyId = config.AccessKeyId,
            Host = host,
            Policy = policyBase64,
            Signature = signature,
            Callback = callbackBase64,
        };
    }

    public async Task<OssUploadCallbackOutput> UploadCallbackAsync(HttpContext httpContext)
    {
        var config = await GetConfigAsync();
        //_logger.LogInformation("请求头：{Headers}", JsonUtil.Serialize(httpContext.Request.Headers));
        var body = await httpContext.Request.ReadBodyAsync();
        //_logger.LogInformation("请求内容：{Body}", body);

        var authorization = httpContext.Request.Headers.Authorization.FirstOrDefault() ?? throw new ArgumentException("未读取到：Authorization");

        var publicKeyUrlBase64 = httpContext.Request.Headers["x-oss-pub-key-url"].FirstOrDefault() ?? throw new ArgumentException("未读取到：x-oss-pub-key-url");
        var publicKeyUrl = Encoding.ASCII.GetString(Convert.FromBase64String(publicKeyUrlBase64));

        var client = CreateHttpClient(config.Host);
        var response = await client.GetAsync(publicKeyUrl);
        response.EnsureSuccessStatusCode();
        var pem = await response.Content.ReadAsStringAsync();
        pem = pem.Replace("-----BEGIN PUBLIC KEY-----\n", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "");
        //var keyBytes = Convert.FromBase64String(pem);

        var data = $"{httpContext.Request.Path.Value}{httpContext.Request.QueryString.Value ?? ""}\n{body}";

        var res = AliyunOssHelper.VerifySignature(pem, data, authorization);
        if (!res) throw new ArgumentException("验签失败");

        var json = body.Replace("\"width\":,", "\"width\":null,").Replace("\"height\":,", "\"height\":null,");
        return JsonUtil.Deserialize<OssUploadCallbackOutput>(json) ?? throw new ArgumentException($"反序列换请求内容发生异常=>{body}");
    }

    public async Task PutObjectAsync(string region, string bucket, string dir, string name, string filePath, bool enableMD5 = false)
    {
        if (!File.Exists(filePath) || Directory.Exists(filePath)) throw new ArgumentException($"无效的文件地址：{filePath}");
        using var fs = File.OpenRead(filePath);
        await PutObjectAsync(region, bucket, dir, name, fs, enableMD5);
    }

    public async Task PutObjectAsync(string region, string bucket, string dir, string name, Stream stream, bool enableMD5 = false)
    {
        var exntesion = Path.GetExtension(name);
        var contentType = GetContentType(exntesion);

        var content = new StreamContent(stream);
        if (stream.CanSeek)
        {
            content.Headers.ContentLength = stream.Length;
            if (enableMD5)
            {
                stream.Seek(0, SeekOrigin.Begin);
                content.Headers.ContentMD5 = await MD5.HashDataAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");

        await SendAsync(HttpMethod.Put, $"{dir}/{name}", region, bucket, content);
    }

    public async Task<string> GetObjectUrlAsync(string region, string bucket, string dir, string name, long expiration, IDictionary<string, string?>? parameters = null)
    {
        var config = await GetConfigAsync();
        var time = DateTime.UtcNow;
        var date = AliyunOssHelper.GetDate(time);
        var timestamp = AliyunOssHelper.GetTimestamp(time);

        var query = new SortedDictionary<string, string?>(StringComparer.Ordinal)
        {
            { "x-oss-signature-version", SignatureVersion },
            { "x-oss-date", timestamp },
            { "x-oss-expires", expiration.ToString() },
            { "x-oss-credential", $"{config.AccessKeyId}/{AliyunOssHelper.GetScope(date, region)}" },
        };
        if (parameters != null)
        {
            foreach (var kvp in parameters)
            {
                query.Add(kvp.Key, kvp.Value);
            }
        }
        var additionalHeaderNames = AliyunOssHelper.GetAdditionalHeaderName(null);
        if (additionalHeaderNames.Count > 0)
        {
            var additionalHeaderNameString = string.Join(';', additionalHeaderNames);
            query.Add("x-oss-additional-headers", additionalHeaderNameString);
        }

        var sign = Sign(config, HttpMethod.Get, $"{dir}/{name}", time, region, bucket, query, null);
        query.Add("x-oss-signature", sign.Signature);
        return Utils.StringUtils.ToQueryString(query, true, true);
    }

    public async Task PutObjectAclAsync(string region, string bucket, string dir, string name, string acl)
    {
        var headers = new SortedDictionary<string, string?>(StringComparer.Ordinal)
        {
            { "x-oss-object-acl", acl }
        };
        var query = new SortedDictionary<string, string?>(StringComparer.Ordinal)
        {
            { "acl", acl }
        };
        await SendAsync(HttpMethod.Put, $"{dir}/{name}", region, bucket, null, query, headers);
    }

    async Task<string> SendAsync(HttpMethod method, string url, string region, string bucket, HttpContent? content = null, SortedDictionary<string, string?>? query = null, SortedDictionary<string, string?>? headers = null)
    {
        var config = await GetConfigAsync();
        var time = DateTime.UtcNow;
        var date = AliyunOssHelper.GetDate(time);
        var timestamp = AliyunOssHelper.GetTimestamp(time);

        headers ??= new SortedDictionary<string, string?>(StringComparer.Ordinal);
        headers.Add("Date", time.ToString("r"));
        headers.Add("x-oss-content-sha256", UnsignedPayload);
        headers.Add("x-oss-date", timestamp);
        if (content != null)
        {
            foreach (var header in content.Headers)
            {
                headers.Add(header.Key, header.Value.FirstOrDefault() ?? "");
            }
        }

        var sign = Sign(config, method, url, time, region, bucket, query, headers);
        var authorization = $"{SignatureVersion} Credential={config.AccessKeyId}/{AliyunOssHelper.GetScope(date, region)}";
        if (!string.IsNullOrEmpty(sign.AdditionalHeaders))
        {
            authorization += $",AdditionalHeaders={sign.AdditionalHeaders}";
        }
        authorization += $",Signature={sign.Signature}";

        var queryString = Utils.StringUtils.ToQueryString(query, true);

        var host = AliyunOssHelper.GetHost(region, bucket);
        var client = CreateHttpClient(host);
        client.Timeout = TimeSpan.FromMinutes(30);
        var request = new HttpRequestMessage(method, url + $"?{queryString}");
        request.Headers.TryAddWithoutValidation("Authorization", authorization);
        foreach (var header in headers)
        {
            if (header.Key.StartsWith("Content-")) continue;
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
        request.Headers.ExpectContinue = true;
        request.Content = content;
        var response = await client.SendAsync(request);
        var res = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new ArgumentException($"请求{ServiceName}发生异常，请联系管理员=>{res}");
        return res;
    }


    static OssSignOutput Sign(AliyunOssConfig config, HttpMethod method, string url, DateTime time, string region, string bucket, SortedDictionary<string, string?>? query, SortedDictionary<string, string?>? headers)
    {
        var date = AliyunOssHelper.GetDate(time);
        var timestamp = AliyunOssHelper.GetTimestamp(time);
        var scope = AliyunOssHelper.GetScope(date, region);

        var additionalHeaderNames = AliyunOssHelper.GetAdditionalHeaderName(headers);
        var additionalHeaderNameString = string.Join(';', additionalHeaderNames);
        var canonicalheaderString = AliyunOssHelper.JoinHeaderToSign(headers, additionalHeaderNames);
        var canonicalQueryString = Utils.StringUtils.ToQueryString(query, true, true);
        var canonicalRequestString = string.Join("\n", method.Method.ToUpper(), $"/{bucket}/{url}", canonicalQueryString, canonicalheaderString, additionalHeaderNameString, UnsignedPayload);
        Console.WriteLine("canonicalRequest: {0}", canonicalRequestString);
        var canonicalRequestHashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(canonicalRequestString));
        var canonicalRequest = Utils.StringUtils.ConvertToHexString(canonicalRequestHashBytes);
        var dataToSign = $"{SignatureVersion}\n{timestamp}\n{scope}\n{canonicalRequest}";
        Console.WriteLine("dataToSign: {0}", dataToSign);
        var signature = ComputeSignature(dataToSign, date, region, config.AccessKeySecret);

        return new OssSignOutput()
        {
            AdditionalHeaders = additionalHeaderNameString,
            Signature = signature,
        };
    }

    static string ComputeSignature(string data, string date, string region, string accessKeySecret)
    {
        var dateKey = HMACSHA256.HashData(Encoding.UTF8.GetBytes("aliyun_v4" + accessKeySecret), Encoding.UTF8.GetBytes(date));
        var regionKey = HMACSHA256.HashData(dateKey, Encoding.UTF8.GetBytes(region));
        var serviceKey = HMACSHA256.HashData(regionKey, Encoding.UTF8.GetBytes("oss"));
        var signingkey = HMACSHA256.HashData(serviceKey, Encoding.UTF8.GetBytes("aliyun_v4_request"));
        var result = HMACSHA256.HashData(signingkey, Encoding.UTF8.GetBytes(data));
        return Utils.StringUtils.ConvertToHexString(result);
    }


}
