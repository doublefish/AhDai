using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Amap.Configs;
using AhDai.Integration.Amap.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Amap;

/// <summary>
/// AmapService
/// </summary>
internal class AmapService(IBaseRedisService redisService, IAmapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AmapConfig, IAmapConfigProvider>(configProvider, httpClientFactory), IAmapService
{
    protected readonly IBaseRedisService _redisService = redisService;

    public async Task<ReverseGeocodeOutput> GetReverseGeocodeAsync(ReverseGeocodeInput input)
    {
        //if (string.IsNullOrEmpty(input.Output)) input.Output = "json";

        var config = await GetConfigAsync();
        var url = $"v3/geocode/regeo";
        var res = await SendAsync<ReverseGeocodeOutput, ReverseGeocodeInput>(config, HttpMethod.Get, url, input);
        if (res.Regeocode == null) throw new Exception("请求高德地图服务发生异常：响应结果中数据为空，请联系管理员");
        return res;
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = $"AhDai:Amap:IpLocation:{ip}";
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
        var url = $"v3/ip";
        var res = await SendAsync<IpLocationOutput, IpLocationInput>(config, HttpMethod.Get, url, input);
        //if (string.IsNullOrEmpty(res.Address) || res.Content == null) throw new Exception("请求高德地图服务发生异常：响应结果中数据为空，请联系管理员");
        return res;
    }

    async Task<TOutput> SendAsync<TOutput, TInput>(AmapConfig config, HttpMethod method, string url, TInput input)
        where TOutput : BaseOutput
        where TInput : BaseInput
    {
        input.Key = config.AccessKey;
        if (method == HttpMethod.Get)
        {
            var queryString = Utils.StringUtils.ObjectToQueryString(input, true);
            url += "?" + queryString;
        }
        var content = method == HttpMethod.Get ? null : JsonContent.Create(input);
        return await SendAsync<TOutput>(config, method, url, content);
    }

    async Task<TOutput> SendAsync<TOutput>(AmapConfig config, HttpMethod method, string url, HttpContent? content)
        where TOutput : BaseOutput
    {
        var client = CreateHttpClient(config.Host);
        return await Utils.HttpUtils.SendAsync<TOutput>(client, method, url, content, "高德地图");
    }
}
