using AhDai.Core.Infrastructure.Redis;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models.Map;
using AhDai.Integration.Baidu.Providers;
using AhDai.Integration.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduMapService
/// </summary>
[Attributes.Service()]
internal class BaiduMapService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IBaiduMapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<BaiduMapConfig, IBaiduMapConfigProvider>(configProvider, httpClientFactory)
    , IBaiduMapService
{
    protected readonly IBaseRedisService _redisService = redisService;
    protected readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

    protected override string ServiceName => "百度地图";

    public async Task<ReverseGeocodingOutput> GetReverseGeocodingAsync(double lat, double lng)
    {
        return await GetReverseGeocodingAsync(new ReverseGeocodingInput() { Location = $"{lat},{lng}" });
    }

    public async Task<ReverseGeocodingOutput> GetReverseGeocodingAsync(ReverseGeocodingInput input)
    {
        if (string.IsNullOrEmpty(input.Language)) input.Language = "zh-cn";
        if (string.IsNullOrEmpty(input.Output)) input.Output = "json";
        await EnsureKeyAsync(input);

        var url = $"reverse_geocoding/v3/";
        return await GetAsync<ReverseGeocodingOutput>(url, input);
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        if (enableCache)
        {
            var config = await GetConfigAsync();
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"BaiduMap:{config.AccessKey}:IpLocation:{ip}");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<IpLocationOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetIpLocationAsync(new IpLocationInput() { Ip = ip });
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, TimeSpan.FromDays(7));
                return res;
            }
        }
        return await GetIpLocationAsync(new IpLocationInput() { Ip = ip });
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(IpLocationInput input)
    {
        await EnsureKeyAsync(input);

        var url = $"location/ip";
        return await GetAsync<IpLocationOutput>(url, input);
    }

    async Task EnsureKeyAsync(BaseInput input)
    {
        if (!string.IsNullOrEmpty(input.Ak)) return;
        var config = await GetConfigAsync();
        if (string.IsNullOrWhiteSpace(config.AccessKey)) throw new InvalidOperationException($"未配置{ServiceName} Key");
        input.Ak = config.AccessKey;
    }
}
