using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Infrastructure.Services;
using AhDai.Integration.Tencent.Configs;
using AhDai.Integration.Tencent.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Tencent;

/// <summary>
/// TianyanchaService
/// </summary>
internal class TencentMapService(IBaseRedisService redisService, ITencentMapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TencentMapConfig, ITencentMapConfigProvider>(configProvider, httpClientFactory), ITencentMapService
{
    protected readonly IBaseRedisService _redisService = redisService;

    public async Task<GeocoderOutput> GetGeocoderAsync(GeocoderInput input)
    {
        var config = await GetConfigAsync();
        var url = "ws/geocoder/v1";
        return await SendAsync<GeocoderOutput, GeocoderInput>(config, HttpMethod.Get, url, input);
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = $"AhDai:TencentMap:IpLocation:{ip}";
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
        var url = "ws/location/v1/ip";
        return await SendAsync<IpLocationOutput, IpLocationInput>(config, HttpMethod.Get, url, input);
    }

    async Task<TOutput> SendAsync<TOutput, TInput>(TencentMapConfig config, HttpMethod method, string url, TInput input)
        where TOutput : class
        where TInput : BaseMapInput
    {
        input.Key = config.Key;
        if (method == HttpMethod.Get)
        {
            var queryString = Utils.StringUtils.ObjectToQueryString(input, true);
            url += "?" + queryString;
        }
        var content = method == HttpMethod.Get ? null : JsonContent.Create(input);
        return await SendAsync<TOutput>(config, method, url, content);
    }

    async Task<TOutput> SendAsync<TOutput>(TencentMapConfig config, HttpMethod method, string url, HttpContent? content)
        where TOutput : class
    {
        var client = CreateHttpClient(config.Host);
        var res = await Utils.HttpUtils.SendAsync<BaseMapOutput<TOutput>>(client, method, url, content, "腾讯地图");
        return res.Result;
    }
}
