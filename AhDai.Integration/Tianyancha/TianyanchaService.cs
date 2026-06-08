using AhDai.Integration.Infrastructure.Services;
using AhDai.Integration.Tianyancha.Configs;
using AhDai.Integration.Tianyancha.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Tianyancha;

/// <summary>
/// TianyanchaService
/// </summary>
internal class TianyanchaService(ITianyanchaConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TianyanchaConfig, ITianyanchaConfigProvider>(configProvider, httpClientFactory), ITianyanchaService
{
    async Task<HttpClient> CreateHttpClientAsync()
    {
        var config = await GetConfigAsync();
        return CreateHttpClient(config.Host, config.Key);
    }

    public async Task<BaseInfoOutput> GetBaseInfoAsync(string id, string name)
    {
        var url = $"services/open/ic/baseinfo/2.0?id={id}&name={name}";
        var client = await CreateHttpClientAsync();
        var response = await client.GetAsync(url);
        var res = await response.Content.ReadFromJsonAsync<BaseOutput<BaseInfoOutput>>() ?? throw new Exception("请求天眼查服务发生异常：解析响应结果失败，请联系管理员");
        if (res.ErrorCode != 0 && res.ErrorCode != 300000) throw new Exception($"请求天眼查服务发生异常：[{res.ErrorCode}]{res.Reason}");
        return res.Result;
    }

    public async Task<PageOutput<TaxpayerOutput>> GetTaxpayerAsync(string keyword, int pageNum = 1, int pageSize = 20)
    {
        var url = $"services/open/m/taxpayer/2.0?keyword={keyword}&pageNum={pageNum}&pageSize={pageSize}";
        var client = await CreateHttpClientAsync();
        var response = await client.GetAsync(url);
        var res = await response.Content.ReadFromJsonAsync<BaseOutput<PageOutput<TaxpayerOutput>>>() ?? throw new Exception("请求天眼查服务发生异常：解析响应结果失败，请联系管理员");
        if (res.ErrorCode != 0 && res.ErrorCode != 300000) throw new Exception($"请求天眼查服务发生异常：[{res.ErrorCode}]{res.Reason}");
        return res.Result;
    }

}
