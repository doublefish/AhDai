using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using AhDai.Integration.Tencent.Configs;
using AhDai.Integration.Tencent.Models;
using AhDai.Integration.Tencent.Providers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Tencent;

/// <summary>
/// TianyanchaService
/// </summary>
[Attributes.Service()]
internal class TencentMapService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, ITencentMapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TencentMapConfig, ITencentMapConfigProvider>(configProvider, httpClientFactory)
    , ITencentMapService
{
    readonly IBaseRedisService _redisService = redisService;
    readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

    protected override string ServiceName => "腾讯地图";


    public async Task<GeocoderOutput> GetGeocoderAsync(GeocoderInput input)
    {
        var config = await GetConfigAsync();
        input.Key = config.Key;
        var url = "ws/geocoder/v1";
        var res = await GetAsync<MapOutput<GeocoderOutput>>(url, input);
        return res.Result;
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"TencentMap:{config.Key}:IpLocation:{ip}");
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
        var config = await GetConfigAsync();
        input.Key = config.Key;
        var url = "ws/location/v1/ip";
        var res = await GetAsync<MapOutput<IpLocationOutput>>(url, input);
        return res.Result;

    }
}
