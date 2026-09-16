using AhDai.Integration.Abstractions;
using AhDai.Integration.Models;
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
    /// <param name="config">配置</param>
    /// <returns></returns>
    public RateLimiter Get(string key, RateLimitConfig config)
    {
        if (string.IsNullOrEmpty(key)) throw new ArgumentException("限流器Key不能为空", nameof(key));
        if (config.RequestsPerSecond <= 0) throw new ArgumentOutOfRangeException("每秒请求数必须大于0");

        return _limiters.GetOrAdd(key, _ => new FixedWindowRateLimiter(
            new FixedWindowRateLimiterOptions
            {
                Window = TimeSpan.FromSeconds(1),
                AutoReplenishment = true,
                PermitLimit = config.RequestsPerSecond,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = config.QueueLimit,
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
