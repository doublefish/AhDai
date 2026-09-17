using AhDai.Integration.Abstractions;

namespace AhDai.Integration.Models;

/// <summary>
/// BaseConfig
/// </summary>
public abstract class BaseConfig : IConfig
{
    /// <summary>
    /// Host
    /// </summary>
    public string Host { get; set; } = default!;
    /// <summary>
    /// 每秒允许的请求数，0 表示不限制
    /// </summary>
    public int RequestsPerSecond { get; set; }
    /// <summary>
    /// 允许排队等待的请求数
    /// </summary>
    public int QueueLimit { get; set; }
}
