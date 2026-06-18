using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Integration.Infrastructure;

/// <summary>
/// BaseService
/// </summary>
public abstract class BaseService : IBaseService
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
    /// 服务名称
    /// </summary>
    protected abstract string ServiceName { get; }

    /// <summary>
    /// BaseService
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        var type = GetType();
        _logger = LoggerUtil.GetLogger(type);
        _httpClientFactory = httpClientFactory;

    }

    /// <summary>
    /// CreateHttpClient
    /// </summary>
    /// <param name="baseAddress"></param>
    /// <param name="authorization"></param>
    /// <param name="scheme"></param>
    /// <returns></returns>
    protected virtual HttpClient CreateHttpClient(string? baseAddress = null, string? authorization = null, string? scheme = null)
    {
        var client = _httpClientFactory.CreateClient();
        if (!string.IsNullOrEmpty(baseAddress))
        {
            client.BaseAddress = new Uri(baseAddress);
        }
        if (!string.IsNullOrEmpty(authorization))
        {
            if (!string.IsNullOrEmpty(scheme))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme ?? "", authorization);
            }
            else
            {
                client.DefaultRequestHeaders.Add("Authorization", authorization);
            }
        }
        client.Timeout = TimeSpan.FromSeconds(30);
        return client;
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="url"></param>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected Task<TOutput> GetAsync<TOutput>(string url, object? query = null, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => GetAsync<TOutput>(null, url, query, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="url"></param>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected Task<TOutput> GetAsync<TOutput>(HttpClient? client, string url, object? query = null, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => SendAsync<TOutput>(client, HttpMethod.Get, url, query, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected Task<TOutput> PostAsync<TOutput>(string url, object? body = null, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => PostAsync<TOutput>(null, url, body, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected Task<TOutput> PostAsync<TOutput>(HttpClient? client, string url, object? body = null, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => SendAsync<TOutput>(client, HttpMethod.Post, url, body, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task<TOutput> SendAsync<TOutput>(HttpMethod method, string url, object? data, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => SendAsync<TOutput>(null, method, url, data, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual async Task<TOutput> SendAsync<TOutput>(HttpClient? client, HttpMethod method, string url, object? data, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
    {
        HttpContent? content = null;
        if (method == HttpMethod.Get)
        {
            if (data != null)
            {
                var queryString = Utils.StringUtils.ObjectToQueryString(data, true);
                url += "?" + queryString;
            }
            //content = JsonContent.Create("");
        }
        else
        {
            content = data != null ? JsonContent.Create(data) : null;
        }
        return await SendContentAsync<TOutput>(client, method, url, content, cancellationToken);
    }


    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task<TOutput> SendContentAsync<TOutput>(HttpMethod method, string url, HttpContent? content, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
        => SendContentAsync<TOutput>(null, method, url, content, cancellationToken);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual async Task<TOutput> SendContentAsync<TOutput>(HttpClient? client, HttpMethod method, string url, HttpContent? content, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
    {
        var request = new HttpRequestMessage(method, url)
        {
            Content = content,
        };
        return await SendAsync<TOutput>(client, request, cancellationToken);
    }

    /// <summary>
    /// SendAsync
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual async Task<TOutput> SendAsync<TOutput>(HttpClient? client, HttpRequestMessage request, CancellationToken cancellationToken = default)
        where TOutput : IBaseOutput
    {
        client ??= CreateHttpClient();
        var response = await client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<TOutput>(cancellationToken) ?? throw new Exception($"请求{ServiceName}返回数据反序列化失败，请联系管理员");
        res.EnsureResult();
        return res;
    }
}

/// <summary>
/// BaseService
/// </summary>
public abstract class BaseService<TConfig, TConfigProvider>(TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService(httpClientFactory), IBaseService<TConfig>
    where TConfig : class, IConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    /// <summary>
    /// _configProvider
    /// </summary>
    protected TConfigProvider _configProvider = configProvider;

    /// <summary>
    /// GetConfigAsync
    /// </summary>
    /// <returns></returns>
    public ValueTask<TConfig> GetConfigAsync() => _configProvider.GetAsync();

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<TOutput> SendAsync<TOutput>(HttpClient? client, HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        if (client == null)
        {
            var config = await GetConfigAsync();
            client ??= CreateHttpClient(config.Host);
        }
        return await base.SendAsync<TOutput>(client, request, cancellationToken);
    }
}
