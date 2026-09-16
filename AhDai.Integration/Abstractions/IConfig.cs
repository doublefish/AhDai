using AhDai.Integration.Models;

namespace AhDai.Integration.Abstractions;

/// <summary>
/// IConfig
/// </summary>
public interface IConfig
{
    /// <summary>
    /// Host
    /// </summary>
    string Host { get; set; }
    /// <summary>
    /// 请求限流配置
    /// </summary>
    RateLimitConfig? RateLimit { get; set; }
}
