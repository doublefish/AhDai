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
    /// <param name="requestsPerSecond">每秒允许的请求数</param>
    /// <param name="queueLimit">允许排队等待的请求数</param>
    /// <returns></returns>
    RateLimiter Get(string key, int requestsPerSecond, int queueLimit);
}
