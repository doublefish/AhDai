using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using AhDai.Integration.Tianyancha.Configs;
using AhDai.Integration.Tianyancha.Models;
using AhDai.Integration.Tianyancha.Providers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Integration.Tianyancha;

/// <summary>
/// TianyanchaService
/// </summary>
[Attributes.Service()]
internal class TianyanchaService(ITianyanchaConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TianyanchaConfig, ITianyanchaConfigProvider>(configProvider, httpClientFactory)
    , ITianyanchaService
{
    protected override string ServiceName => "天眼查";


    public async Task<BaseInfoOutput> GetBaseInfoAsync(string id, string name)
    {
        var url = $"services/open/ic/baseinfo/2.0?id={id}&name={name}";
        var res = await GetAsync<Output<BaseInfoOutput>>(url);
        return res.Result;
    }

    public async Task<PageOutput<TaxpayerOutput>> GetTaxpayerAsync(string keyword, int pageNum = 1, int pageSize = 20)
    {
        var url = $"services/open/m/taxpayer/2.0?keyword={keyword}&pageNum={pageNum}&pageSize={pageSize}";
        var res = await GetAsync<Output<PageOutput<TaxpayerOutput>>>(url);
        return res.Result;
    }

    protected override async Task<TOutput> SendAsync<TOutput>(HttpClient? client, HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        if (client == null)
        {
            var config = await GetConfigAsync();
            client = CreateHttpClient(config.Host, config.Key);
        }
        return await base.SendAsync<TOutput>(client, request, cancellationToken);
    }
}
