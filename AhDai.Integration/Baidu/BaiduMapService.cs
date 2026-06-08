using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using AhDai.Integration.Infrastructure.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduMapService
/// </summary>
internal class BaiduMapService(IBaseRedisService redisService, IBaiduMapConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<BaiduMapConfig, IBaiduMapConfigProvider>(configProvider, httpClientFactory), IBaiduMapService
{
    protected readonly IBaseRedisService _redisService = redisService;

    public async Task<ReverseGeocodingOutput> GetReverseGeocodingAsync(ReverseGeocodingInput input)
    {
        if (string.IsNullOrEmpty(input.Language)) input.Language = "zh-cn";
        if (string.IsNullOrEmpty(input.Output)) input.Output = "json";

        var config = await GetConfigAsync();
        var url = $"reverse_geocoding/v3/";
        var res = await SendAsync<ReverseGeocodingOutput, ReverseGeocodingInput>(config, HttpMethod.Get, url, input);
        if (res.Result == null) throw new Exception("请求百度地图服务发生异常：响应结果中数据为空，请联系管理员");
        return res;
    }

    public async Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false)
    {
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = $"AhDai:BaiduMap:IpLocation:{ip}";
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
        var url = $"location/ip";
        var res = await SendAsync<IpLocationOutput, IpLocationInput>(config, HttpMethod.Get, url, input);
        if (string.IsNullOrEmpty(res.Address) || res.Content == null) throw new Exception("请求百度地图服务发生异常：响应结果中数据为空，请联系管理员");
        return res;
    }

    async Task<TOutput> SendAsync<TOutput, TInput>(BaiduMapConfig config, HttpMethod method, string url, TInput input)
        where TOutput : BaseMapOutput
        where TInput : BaseMapInput
    {
        input.Ak = config.AccessKey;
        if (method == HttpMethod.Get)
        {
            var queryString = Utils.StringUtils.ObjectToQueryString(input, true);
            url += "?" + queryString;
        }
        var content = method == HttpMethod.Get ? null : JsonContent.Create(input);
        return await SendAsync<TOutput>(config, method, url, content);
    }

    async Task<TOutput> SendAsync<TOutput>(BaiduMapConfig config, HttpMethod method, string url, HttpContent? content)
        where TOutput : BaseMapOutput
    {
        var client = CreateHttpClient(config.Host);
        return await Utils.HttpUtils.SendAsync<TOutput>(client, method, url, content, "百度地图");
    }
}
