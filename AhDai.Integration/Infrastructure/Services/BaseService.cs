using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Infrastructure.Services;

/// <summary>
/// BaseService
/// </summary>
internal class BaseService : IBaseService
{
    protected readonly ILogger _logger;
    protected readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _logger = LoggerUtil.GetLogger(GetType());
        _httpClientFactory = httpClientFactory;
    }

    protected virtual HttpClient CreateHttpClient(string? baseAddress = null, string? authorization = null)
    {
        var client = _httpClientFactory.CreateClient();
        if (!string.IsNullOrEmpty(baseAddress))
        {
            client.BaseAddress = new Uri(baseAddress);
        }
        if (!string.IsNullOrEmpty(authorization))
        {
            client.DefaultRequestHeaders.Add("Authorization", authorization);
        }
        client.Timeout = TimeSpan.FromSeconds(30);
        return client;
    }
}

/// <summary>
/// BaseService
/// </summary>
internal class BaseService<TConfig, TConfigProvider>(TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService(httpClientFactory), IBaseService<TConfig>
    where TConfig : class, IConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    protected TConfigProvider _configProvider = configProvider;
    protected string _configName = typeof(TConfig).Name.Replace("Config", "");

    public Task<TConfig> GetConfigAsync() => _configProvider.GetAsync();
}
