using AhDai.Integration.Models;
using System.Threading.RateLimiting;

namespace AhDai.Integration.Abstractions;

/// <summary>
/// RateLimiterProvider
/// </summary>
public interface IRateLimiterProvider
{
    /// <summary>
    /// 获取限流器
    /// </summary>
    /// <param name="key">限流器Key</param>
    /// <param name="config">配置</param>
    /// <returns></returns>
    RateLimiter Get(string key, RateLimitConfig config);
}
