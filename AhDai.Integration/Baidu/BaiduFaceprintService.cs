using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using AhDai.Integration.Baidu.Providers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduFaceprintService
/// </summary>
[Attributes.Service()]
internal class BaiduFaceprintService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IBaiduFaceprintConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseBaiduService<BaiduFaceprintConfig, IBaiduFaceprintConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IBaiduFaceprintService
{
    protected override string ServiceName => "百度人脸识别";


    public async Task<VerifyTokenOutput> GetVerifyTokenAsync(string accessToken)
    {
        var config = await GetConfigAsync();
        var url = $"rpc/2.0/brain/solution/faceprint/verifyToken/generate?access_token={accessToken}";
        var input = new Dictionary<string, object>()
        {
            ["plan_id"] = config.FaceprintPlanId
        };
        var res = await PostAsync<FaceprintH5Output<VerifyTokenOutput>>(url, input);
        return res.Result;
    }

    public async Task SubmitIdCardAsync(string accessToken, IdCardSumbitInput input)
    {
        var url = $"rpc/2.0/brain/solution/faceprint/idcard/submit?access_token={accessToken}";
        await PostAsync<FaceprintH5Output<int?>>(url, input);
    }

    public async Task<SimpleResultOutput> GetSimpleResultAsync(string token)
    {
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/simple?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        var res = await PostAsync<FaceprintH5Output<SimpleResultOutput>>(url, input);
        return res.Result;
    }

    public async Task<MediaResultOutput> GetMediaResultAsync(string token)
    {
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/media/query?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        var res = await PostAsync<FaceprintH5Output<MediaResultOutput>>(url, input);
        return res.Result;
    }

    public async Task<DetailResultOutput> GetDetailResultAsync(string token)
    {
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/detail?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        var res = await PostAsync<FaceprintH5Output<DetailResultOutput>>(url, input);
        return res.Result;
    }

    public async Task<AllResultOutput> GetAllResultAsync(string token)
    {
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/getall?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        var res = await PostAsync<FaceprintH5Output<AllResultOutput>>(url, input);
        return res.Result;
    }

    public async Task<string> GenerateUrlAsync(string token, string? callbackUrl = null, string? successUrl = null, string? failedUrl = null)
    {
        var config = await GetConfigAsync();
        var url = $"{config.FaceprintUrl}?token={token}";
        if (!string.IsNullOrEmpty(callbackUrl))
        {
            url += $"&callbackUrl={Uri.EscapeDataString(callbackUrl)}";
        }
        if (!string.IsNullOrEmpty(successUrl))
        {
            url += $"&successUrl={Uri.EscapeDataString(successUrl)}";
        }
        if (!string.IsNullOrEmpty(failedUrl))
        {
            url += $"&failedUrl={Uri.EscapeDataString(failedUrl)}";
        }
        return url;
    }
}
