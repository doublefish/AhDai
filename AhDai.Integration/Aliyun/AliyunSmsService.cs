using AhDai.Core.Utils;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunService
/// </summary>
internal class AliyunSmsService(IAliyunSmsConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AliyunSmsConfig, IAliyunSmsConfigProvider>(configProvider, httpClientFactory), IAliyunSmsService
{
    public async Task<BaseSmsOutput> SendAsync(string phoneNumber, string templateCode, IDictionary<string, string> templateParam, string signName)
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
            { "Timestamp", DateTime.UtcNow.ToString(Core.Consts.DateTimeFormat.Iso8601Utc) },
            { "Version", "2017-05-25" }
        };
        return await SendAsync<BaseSmsOutput>(config, HttpMethod.Post, input);
    }

    async Task<TOutput> SendAsync<TOutput>(AliyunSmsConfig config, HttpMethod method, SortedDictionary<string, string?> body) where TOutput : BaseSmsOutput
    {
        var bodyString = Utils.StringUtils.ToQueryString(body, true, true);
        var dataToSign = $"{method.Method.ToUpper()}&%2F&" + Uri.EscapeDataString(bodyString);
        var signature = ComputeSignature(dataToSign, config.AccessKeySecret);
        body.Add("Signature", signature);

        var client = CreateHttpClient(config.Host);
        var content = new FormUrlEncodedContent(body);
        var response = await client.PostAsync("", content);
        var res = await response.Content.ReadFromJsonAsync<TOutput>() ?? throw new Exception("请求阿里云短信服务发生异常：解析响应结果失败，请联系管理员");
        if (res.Code != "OK") throw new Exception($"请求阿里云短信服务发生异常：[{res.Code}]{res.Message}，请联系管理员");
        return res;
    }

    static string ComputeSignature(string data, string accessKeySecret)
    {
        using var sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(accessKeySecret + "&"));
        return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(data)));
    }
}
