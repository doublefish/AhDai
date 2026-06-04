using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduService
/// </summary>
internal class BaiduService(IBaseRedisService redisService, IBaiduConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<BaiduConfig, IBaiduConfigProvider>(configProvider, httpClientFactory), IBaiduService
{
    readonly IBaseRedisService _redisService = redisService;

    public async Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = $"AhDai:Baidu:AccessToken:{config.AppId}";
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<AccessTokenOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetAccessTokenAsync(false);
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, TimeSpan.FromSeconds(res.ExpiresIn - 30));
                return res;
            }
        }
        var url = $"oauth/2.0/token?client_id={config.ApiKey}&client_secret={config.AppSecret}&grant_type=client_credentials";
        return await SendAsync<AccessTokenOutput, string>(config, HttpMethod.Post, url, "");
    }

    protected virtual async Task<TOutput> SendAsync<TOutput, TInput>(BaiduConfig config, HttpMethod method, string url, TInput? input = null)
        where TOutput : IBaseOutput
        where TInput : class
    {
        var content = method == HttpMethod.Get ? JsonContent.Create("") : JsonContent.Create(input);
        return await SendAsync<TOutput>(config, method, url, content);
    }

    protected virtual async Task<TOutput> SendAsync<TOutput>(BaiduConfig config, HttpMethod method, string url, HttpContent? content)
        where TOutput : IBaseOutput
    {
        var client = CreateHttpClient(config.Host);
        return await Utils.HttpUtils.SendAsync<TOutput>(client, method, url, content, "百度云");
    }
}
