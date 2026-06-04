using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduFaceprintService
/// </summary>
internal class BaiduFaceprintService(IBaseRedisService redisService, IBaiduConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaiduService(redisService, configProvider, httpClientFactory), IBaiduFaceprintService
{
    public async Task<VerifyTokenOutput> GetVerifyTokenAsync(string accessToken)
    {
        var config = await GetConfigAsync();
        var url = $"rpc/2.0/brain/solution/faceprint/verifyToken/generate?access_token={accessToken}";
        var input = new Dictionary<string, object>()
        {
            ["plan_id"] = config.FaceprintPlanId
        };
        return await SendH5Async<VerifyTokenOutput, Dictionary<string, object>>(config, HttpMethod.Post, url, input);
    }

    public async Task SubmitIdCardAsync(string accessToken, IdCardSumbitInput input)
    {
        var config = await GetConfigAsync();
        var url = $"rpc/2.0/brain/solution/faceprint/idcard/submit?access_token={accessToken}";
        await SendH5Async<int?, IdCardSumbitInput>(config, HttpMethod.Post, url, input);
    }

    public async Task<SimpleResultOutput> GetSimpleResultAsync(string token)
    {
        var config = await GetConfigAsync();
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/simple?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        return await SendH5Async<SimpleResultOutput, Dictionary<string, object>>(config, HttpMethod.Post, url, input);
    }

    public async Task<MediaResultOutput> GetMediaResultAsync(string token)
    {
        var config = await GetConfigAsync();
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/media/query?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        return await SendH5Async<MediaResultOutput, Dictionary<string, object>>(config, HttpMethod.Post, url, input);
    }

    public async Task<DetailResultOutput> GetDetailResultAsync(string token)
    {
        var config = await GetConfigAsync();
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/detail?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        return await SendH5Async<DetailResultOutput, Dictionary<string, object>>(config, HttpMethod.Post, url, input);
    }

    public async Task<AllResultOutput> GetAllResultAsync(string token)
    {
        var config = await GetConfigAsync();
        var accessToken = await GetAccessTokenAsync(true);
        var url = $"rpc/2.0/brain/solution/faceprint/result/getall?access_token={accessToken.AccessToken}";
        var input = new Dictionary<string, object>()
        {
            ["verify_token"] = token
        };
        return await SendH5Async<AllResultOutput, Dictionary<string, object>>(config, HttpMethod.Post, url, input);
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

    async Task<TOutput> SendH5Async<TOutput, TInput>(BaiduConfig config, HttpMethod method, string url, TInput? input)
        where TInput : class
    {
        var res = await SendAsync<BaseFaceprintH5Output<TOutput>, TInput>(config, method, url, input);
        res.EnsureResult();
        return res.Result;
    }

}
