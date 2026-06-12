using AhDai.Core.Utils;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using AhDai.Integration.Aliyun.Providers;
using AhDai.Integration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunService
/// </summary>
[Attributes.Service()]
internal class AliyunSmsService(IAliyunSmsConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AliyunSmsConfig, IAliyunSmsConfigProvider>(configProvider, httpClientFactory)
    , IAliyunSmsService
{
    protected override string ServiceName => "阿里云短信";


    public async Task<SmsOutput> SendAsync(string phoneNumber, string templateCode, IDictionary<string, string> templateParam, string signName)
    {
        var config = await GetConfigAsync();
        var input = new SortedDictionary<string, string?>(StringComparer.Ordinal)
        {
            { "AccessKeyId", config.AccessKeyId },
            { "Action", "SendSms" },
            { "Format", "JSON" },
            { "PhoneNumbers", phoneNumber },
            //{ "RegionId", "cn-hangzhou" },
            { "SignName", signName },
            { "TemplateCode", templateCode },
            { "TemplateParam", JsonUtil.Serialize(templateParam) },
            { "SignatureMethod", "HMAC-SHA1" },
            { "SignatureNonce", Utils.StringUtils.GenerateRandomString(32) },
            { "SignatureVersion", "1.0" },
            { "Timestamp", DateTime.UtcNow.ToString(Core.Consts.DateTimeFormats.Iso8601Utc) },
            { "Version", "2017-05-25" }
        };
        return await SendAsync<SmsOutput>(config, HttpMethod.Post, input);
    }

    async Task<TOutput> SendAsync<TOutput>(AliyunSmsConfig config, HttpMethod method, SortedDictionary<string, string?> body)
        where TOutput : SmsOutput
    {
        var bodyString = Utils.StringUtils.ToQueryString(body, true, true);
        var dataToSign = $"{method.Method.ToUpper()}&%2F&" + Uri.EscapeDataString(bodyString);
        var signature = ComputeSignature(dataToSign, config.AccessKeySecret);
        body.Add("Signature", signature);

        var request = new HttpRequestMessage(HttpMethod.Post, "")
        {
            Content = new FormUrlEncodedContent(body),
        };
        return await SendAsync<TOutput>(null, request);
    }

    static string ComputeSignature(string data, string accessKeySecret)
    {
        using var sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(accessKeySecret + "&"));
        return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(data)));
    }
}
