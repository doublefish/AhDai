using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

internal class AliyunService<TConfig, TConfigProvider>(TConfigProvider configProvider, IHttpClientFactory httpClientFactory, string version, int authType)
    : BaseService<TConfig, TConfigProvider>(configProvider, httpClientFactory)
    where TConfig : Configs.AliyunConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    readonly string _version = version;
    readonly int _authType = authType;

    internal async Task<TOutput> SendAsync<TOutput>(HttpMethod method, string action
        , HttpContent? content
        , SortedDictionary<string, string?>? query = null
        , SortedDictionary<string, string?>? headers = null)
    {
        var config = await GetConfigAsync();
        var time = DateTime.UtcNow;
        var timestamp = time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);

        query ??= new SortedDictionary<string, string?>(StringComparer.Ordinal);

        headers ??= new SortedDictionary<string, string?>(StringComparer.Ordinal);
        headers.Add("host", config.Host[8..]);
        headers.Add("x-acs-action", action);
        headers.Add("x-acs-content-sha256", "");
        headers.Add("x-acs-date", timestamp);
        headers.Add("x-acs-signature-nonce", Utils.StringUtils.GenerateRandomString(16));
        headers.Add("x-acs-version", _version);
        if (content != null)
        {
            foreach (var header in content.Headers)
            {
                headers.Add(header.Key, header.Value.FirstOrDefault() ?? "");
            }
        }

        var requestPayloadBytes = content != null ? await content.ReadAsByteArrayAsync() : Encoding.UTF8.GetBytes("");
        var requestPayloadHashBytes = SHA256.HashData(requestPayloadBytes);
        var requestPayloadString = Utils.StringUtils.ConvertToHexString(requestPayloadHashBytes);
        headers["x-acs-content-sha256"] = requestPayloadString;

        var canonicalheaderBuilder = new StringBuilder();
        var signedHeaderNameBuilder = new StringBuilder();
        foreach (var header in headers)
        {
            if (header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase)
                || header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)
                || header.Key.StartsWith("x-acs-", StringComparison.OrdinalIgnoreCase))
            {
                var lowerKey = header.Key.ToLower();
                canonicalheaderBuilder.Append($"{lowerKey}:{header.Value}\n");
                signedHeaderNameBuilder.Append(lowerKey).Append(';');
            }
        }
        var canonicalheaderString = canonicalheaderBuilder.ToString();
        var signedHeaderNameString = signedHeaderNameBuilder.ToString().TrimEnd(';');
        var canonicalQueryString = Utils.StringUtils.ToQueryString(query, true) + $"{(_authType == 0 ? "&" : "")}action={action}";
        var canonicalRequestString = string.Join("\n", method.Method.ToUpper(), "/", canonicalQueryString, canonicalheaderString, signedHeaderNameString, requestPayloadString);
        Console.WriteLine("canonicalRequest: {0}", canonicalRequestString);
        var canonicalRequestHashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(canonicalRequestString));
        var canonicalRequest = Utils.StringUtils.ConvertToHexString(canonicalRequestHashBytes);
        var dataToSign = $"ACS3-HMAC-SHA256\n{canonicalRequest}";
        Console.WriteLine("dataToSign: {0}", dataToSign);
        var sign = ComputeSignature(dataToSign, config.AccessKeySecret);
        var authorization = $"ACS3-HMAC-SHA256 Credential={config.AccessKeyId},SignedHeaders={signedHeaderNameString},Signature={sign}";

        var client = CreateHttpClient(config.Host);
        client.Timeout = TimeSpan.FromMinutes(30);
        var request = new HttpRequestMessage(method, $"?{canonicalQueryString}");
        request.Headers.TryAddWithoutValidation("Authorization", authorization);
        foreach (var header in headers)
        {
            if (header.Key.StartsWith("Content-")) continue;
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
        request.Content = content;
        var response = await client.SendAsync(request);
        var res = await response.Content.ReadFromJsonAsync<TOutput>() ?? throw new Exception("请求阿里云点播服务发生异常：解析响应结果失败，请联系管理员");
        //if (res.Code != "OK") throw new Exception($"请求阿里云点播服务发生异常：[{res.Code}]{res.Message}，请联系管理员");
        return res;
    }

    static string ComputeSignature(string data, string accessKeySecret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(accessKeySecret));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Utils.StringUtils.ConvertToHexString(hashBytes);
    }
}
