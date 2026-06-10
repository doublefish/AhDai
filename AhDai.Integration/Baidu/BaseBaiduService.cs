using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Models;
using AhDai.Integration.Infrastructure;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaseBaiduService
/// </summary>
internal abstract class BaseBaiduService<TConfig, TConfigProvider>(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TConfig, TConfigProvider>(configProvider, httpClientFactory)
    , IBaseBaiduService<TConfig>
    where TConfig : Configs.BaseBaiduConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    protected readonly IBaseRedisService _redisService = redisService;
    protected readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

    public async Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"Baidu:{config.AppId}:AccessToken");
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
        return await PostAsync<AccessTokenOutput>(url);
    }
}
