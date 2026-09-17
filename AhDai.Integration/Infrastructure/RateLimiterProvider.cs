using AhDai.Integration.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace AhDai.Integration.Infrastructure;

internal class RateLimiterProvider : IRateLimiterProvider, IDisposable
{
    private readonly ConcurrentDictionary<string, RateLimiter> _limiters = new();

    /// <summary>
    /// 获取限流器
    /// </summary>
    /// <param name="key"></param>
    /// <param name="requestsPerSecond">每秒允许的请求数</param>
    /// <param name="queueLimit">允许排队等待的请求数</param>
    /// <returns></returns>
    public RateLimiter Get(string key, int requestsPerSecond, int queueLimit)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(requestsPerSecond);

        return _limiters.GetOrAdd(key, _ => new FixedWindowRateLimiter(
            new FixedWindowRateLimiterOptions
            {
                Window = TimeSpan.FromSeconds(1),
                AutoReplenishment = true,
                PermitLimit = requestsPerSecond,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = queueLimit,
            }));
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        foreach (var limiter in _limiters.Values)
        {
            limiter.Dispose();
        }
        _limiters.Clear();
    }
}
