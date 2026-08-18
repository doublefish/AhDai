using AhDai.Core.Infrastructure.Redis;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Amap.Configs;
using AhDai.Integration.Amap.Models;
using AhDai.Integration.Amap.Providers;
using AhDai.Integration.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Amap;

/// <summary>
/// AmapService
/// </summary>
[Attributes.Service()]
internal class AmapService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IAmapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AmapConfig, IAmapConfigProvider>(configProvider, httpClientFactory), IAmapService
{
    protected readonly IBaseRedisService _redisService = redisService;
    protected readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

    protected override string ServiceName => "高德地图";

    public async Task<ReverseGeocodeOutput> GetReverseGeocodeAsync(double lat, double lng)
    {
        return await GetReverseGeocodeAsync(new ReverseGeocodeInput() { Location = $"{lng},{lat}" });
    }

    public async Task<ReverseGeocodeOutput> GetReverseGeocodeAsync(ReverseGeocodeInput input)
    {
        //if (string.IsNullOrEmpty(input.Output)) input.Output = "json";
        await EnsureKeyAsync(input);

        var url = $"v3/geocode/regeo";
        var res = await GetAsync<ReverseGeocodeOutput>(url, input);
        return res;
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        if (enableCache)
        {
            var config = await GetConfigAsync();
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"Amap:{config.AccessKey}:IpLocation:{ip}");
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

        var url = $"v3/ip";
        var res = await GetAsync<IpLocationOutput>(url, input);
        return res;
    }

    async Task EnsureKeyAsync(BaseInput input)
    {
        if (!string.IsNullOrEmpty(input.Key)) return;
        var config = await GetConfigAsync();
        if (string.IsNullOrWhiteSpace(config.AccessKey)) throw new InvalidOperationException($"未配置{ServiceName} Key");
        input.Key = config.AccessKey;
    }
}
