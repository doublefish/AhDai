using AhDai.Core.Converters;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.AntChain.Models;
using AhDai.Integration.Infrastructure;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using AhDai.Integration.WeChat.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatPayService
/// </summary>
[Attributes.Service()]
internal class WeChatPayService(IWeChatPayConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<WeChatPayConfig, IWeChatPayConfigProvider>(configProvider, httpClientFactory)
    , IWeChatPayService
{
    static JsonSerializerOptions? _JsonOptions;
    static JsonSerializerOptions JsonOptions
    {
        get
        {
            _JsonOptions ??= new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                Converters = { new DateTimeJsonConverter(Core.Consts.DateTimeFormats.Iso8601WithOffset) },
            };
            return _JsonOptions;
        }
    }

    protected override string ServiceName => "微信支付";


    public async Task<H5OrderOutput> CreateH5OrderAsync(H5OrderInput input)
    {
        var config = await GetConfigAsync();
        if (string.IsNullOrEmpty(input.NotifyUrl)) input.NotifyUrl = config.NotifyUrl;
        ArgumentException.ThrowIfNullOrEmpty(input.NotifyUrl);

        input.AppId = config.AppId;
        input.MchId = config.MchId;
        input.TimeExpire = DateTime.Now.AddHours(2);
        input.AmountInfo = new AmountInput()
        {
            Total = (int)(input.Amount * 100),
        };
        var url = "/v3/pay/transactions/h5";
        return await PostAsync<H5OrderOutput>(url, input);
    }

    public async Task<NativeOrderOutput> CreateNativeOrderAsync(NativeOrderInput input)
    {
        var config = await GetConfigAsync();
        if (string.IsNullOrEmpty(input.NotifyUrl)) input.NotifyUrl = config.NotifyUrl;
        ArgumentException.ThrowIfNullOrEmpty(input.NotifyUrl);

        input.AppId = config.AppId;
        input.MchId = config.MchId;
        //input.TimeExpire = DateTime.Now.AddHours(2);
        input.AmountInfo = new AmountInput()
        {
            Total = (int)(input.Amount * 100),
        };
        var url = "/v3/pay/transactions/native";
        return await PostAsync<NativeOrderOutput>(url, input);
    }

    public async Task<object> DownloadBillAsync(DateTime date, string type = "SUCCESS")
    {
        var config = await GetConfigAsync();
        var input = new SortedDictionary<string, string?>()
        {
            { "appid", config.AppId },
            { "mch_id", config.MchId },
            { "nonce_str", Utils.StringUtils.GenerateRandomString(16) },
            //{ "sign", "" },
            { "sign_type", "MD5" },
            { "bill_date", date.ToString("yyyyMMdd") },
            { "bill_type", type },
            { "tar_type", "GZIP" },
        };
        var sign = SignData2(input, config.ApiKey);
        input.Add("sign", sign);

        var xml = new XElement("xml", input.Select(kvp => new XElement(kvp.Key, kvp.Value)));

        var url = "/pay/downloadbill";
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(xml.ToString(), Encoding.UTF8, new MediaTypeHeaderValue("application/xml"))
        };
        return await SendAsync<DownloadBillOutput>(null, request);
    }

    public async Task<OrderNotifyOutput> GetNotifyResultAsync(HttpContext httpContext)
    {
        if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("请求头：{Headers}", JsonUtil.Serialize(httpContext.Request.Headers));

        var timestamp = httpContext.Request.Headers["Wechatpay-Timestamp"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到时间戳");
        var seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - timestamp.ToInt64(0);
        if (seconds > 300) throw new ArgumentException("验签失败：已过期");
        var nonce = httpContext.Request.Headers["Wechatpay-Nonce"].FirstOrDefault() ?? "";
        var serialNo = httpContext.Request.Headers["Wechatpay-Serial"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到证书序列号");
        var signature = httpContext.Request.Headers["Wechatpay-Signature"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到签名");

        httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
        using var requestReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
        var bodyString = await requestReader.ReadToEndAsync();
        if (string.IsNullOrEmpty(bodyString)) throw new ArgumentException("验签失败：未读取到内容");

        var config = await GetConfigAsync();
        if (serialNo != config.SerialNo) throw new ArgumentException("验签失败：证书序列号不一致");

        var dataToSign = string.Join("\n", timestamp, nonce, bodyString, "");
        var cert = await GetCertOrKeyAsync(config, 3);
        var res = VerifyData(dataToSign, signature, cert);
        if (!res) throw new ArgumentException("验签失败");

        var output = JsonUtil.Deserialize<OrderNotifyOutput>(bodyString) ?? throw new ArgumentException("反序列化失败");
        var resourceJson = Decrypt(output.Resource.Nonce, output.Resource.AssociatedData, output.Resource.Ciphertext, config.ApiKey);
        output.DecrypResource = JsonUtil.Deserialize<OrderNotifyResourceDecryptOutput>(resourceJson) ?? throw new ArgumentException("反序列换解密后的资源数据失败");
        return output;
    }

    protected override async Task<TOutput> SendAsync<TOutput>(HttpMethod method, string url, object? data = null, CancellationToken cancellationToken = default)
    {
        var config = await GetConfigAsync();
        var request = new HttpRequestMessage(method, url);
        var body = data != null ? JsonUtil.Serialize(data, JsonOptions) : "";
        if (method != HttpMethod.Get && data != null)
        {
            request.Content = new StringContent(body, new MediaTypeHeaderValue("application/json"));
        }

        var mchid = config.MchId;
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var nonce = Utils.StringUtils.GenerateRandomString(16);

        var serialNo = config.MchSerialNo;
        var dataToSign = string.Join("\n", method.Method.ToUpper(), url, timestamp, nonce, body, "");
        var key = await GetCertOrKeyAsync(config, 2);
        var signature = SignData(dataToSign, key);

        var authorization = $"WECHATPAY2-SHA256-RSA2048 mchid=\"{mchid}\",serial_no=\"{serialNo}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",signature=\"{signature}\"";

        var client = CreateHttpClient(config.Host);
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("User-Agent", "lsl-wechat-pay");
        return await SendAsync<TOutput>(client, request, cancellationToken);
    }

    /// <summary>
    /// 获取密钥
    /// </summary>
    /// <param name="config"></param>
    /// <param name="type">1-商户证书，2-商户私钥，3-平台证书</param>
    /// <returns></returns>
    static async Task<string> GetCertOrKeyAsync(WeChatPayConfig config, int type)
    {
        if (type == 1)
        {
            var text = await File.ReadAllTextAsync(config.MchCertPath);
            return text.Replace("-----BEGIN CERTIFICATE-----", "").Replace("-----END CERTIFICATE-----", "").Trim();
        }
        else if (type == 2)
        {
            var text = await File.ReadAllTextAsync(config.MchKeyPath);
            return text;
        }
        else if (type == 3)
        {
            var text = await File.ReadAllTextAsync(config.CertPath);
            return text.Replace("-----BEGIN CERTIFICATE-----", "").Replace("-----END CERTIFICATE-----", "").Trim();
        }
        else
        {
            throw new ArgumentException("无效的类型");
        }
    }

    static string SignData(string data, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey);
        var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signatureBytes);
    }

    static bool VerifyData(string data, string signature, string certificate)
    {
        var bytes = Convert.FromBase64String(certificate);
#if NET9_0_OR_GREATER
        var cert = X509CertificateLoader.LoadCertificate(bytes);
#else
        var cert = new X509Certificate2(bytes);
#endif
        using var rsa = cert.GetRSAPublicKey() ?? throw new ArgumentException("读取公钥失败");
        return rsa.VerifyData(Encoding.UTF8.GetBytes(data), Convert.FromBase64String(signature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    static string Decrypt(string nonce, string? associatedData, string ciphertext, string key)
    {
        var nonceBytes = Encoding.UTF8.GetBytes(nonce);
        var associatedDataBytes = associatedData == null ? null : Encoding.UTF8.GetBytes(associatedData);
        var encryptedBytes = Convert.FromBase64String(ciphertext);
        var cipherBytes = encryptedBytes[..^16];
        var tag = encryptedBytes[^16..];
        var plaintextBytes = new byte[cipherBytes.Length];
        using var aes = new AesGcm(Encoding.UTF8.GetBytes(key), tag.Length);
        aes.Decrypt(nonceBytes, cipherBytes, tag, plaintextBytes, associatedDataBytes);
        return Encoding.UTF8.GetString(plaintextBytes);
    }

    static string SignData2(SortedDictionary<string, string?> data, string key)
    {
        var dataToSign = Utils.StringUtils.ToQueryString(data, true);
        dataToSign += $"key={key}";

        var hash = MD5.HashData(Encoding.UTF8.GetBytes(dataToSign));
        return Convert.ToBase64String(hash).ToUpper();
    }
}
