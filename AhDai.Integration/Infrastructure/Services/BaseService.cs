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
public class BaseService : IBaseService
{
    /// <summary>
    /// _logger
    /// </summary>
    protected readonly ILogger _logger;
    /// <summary>
    /// _httpClientFactory
    /// </summary>
    protected readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// BaseService
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _logger = LoggerUtil.GetLogger(GetType());
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// CreateHttpClient
    /// </summary>
    /// <param name="baseAddress"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
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
public class BaseService<TConfig, TConfigProvider>(TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService(httpClientFactory), IBaseService<TConfig>
    where TConfig : class, IConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    /// <summary>
    /// _configProvider
    /// </summary>
    protected TConfigProvider _configProvider = configProvider;
    /// <summary>
    /// _configName
    /// </summary>
    protected string _configName = typeof(TConfig).Name.Replace("Config", "");

    /// <summary>
    /// GetConfigAsync
    /// </summary>
    /// <returns></returns>
    public Task<TConfig> GetConfigAsync() => _configProvider.GetAsync();
}
