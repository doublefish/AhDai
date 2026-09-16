using AhDai.Integration.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Integration.Infrastructure;

/// <summary>
/// BaseRateLimitService
/// </summary>
public abstract class BaseRateLimitService<TConfig, TConfigProvider>(TConfigProvider configProvider, IHttpClientFactory httpClientFactory, IRateLimiterProvider rateLimiterProvider)
    : BaseService<TConfig, TConfigProvider>(configProvider, httpClientFactory)
    where TConfig : class, IConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    /// <summary>
    /// _rateLimiterProvider
    /// </summary>
    protected IRateLimiterProvider _rateLimiterProvider = rateLimiterProvider;

    /// <summary>
    /// RateLimiterKey
    /// </summary>
    protected virtual string RateLimiterKey => ServiceName;

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="client"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> ExecuteAsync(HttpClient? client, Func<HttpClient, Task<HttpResponseMessage>> action, CancellationToken cancellationToken = default)
    {
        var config = await GetConfigAsync();

        if (client == null)
        {
            client ??= CreateHttpClient(config.Host);
        }

        if (config.RateLimit?.RequestsPerSecond > 0)
        {
            var rateLimiter = _rateLimiterProvider.Get(RateLimiterKey, config.RateLimit);
            using var lease = await rateLimiter.AcquireAsync(permitCount: 1, cancellationToken);

            if (!lease.IsAcquired)
            {
                _logger.LogError("{ServiceName} API 限流队列已满，无法获取请求许可", ServiceName);
                throw new TimeoutException($"等待{ServiceName}API限流许可失败：队列已满");
            }
        }

        return await base.ExecuteAsync(client, action, cancellationToken);
    }
}

