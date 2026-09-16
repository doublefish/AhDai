namespace AhDai.Integration.Models;

/// <summary>
/// RateLimitConfig
/// </summary>
public class RateLimitConfig
{
    /// <summary>
    /// 每秒允许的请求数
    /// </summary>
    public int RequestsPerSecond { get; set; }
    /// <summary>
    /// 允许排队等待的请求数
    /// </summary>
    public int QueueLimit { get; set; }
}
