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
    /// 每秒允许的请求数，0 表示不限制
    /// </summary>
    int RequestsPerSecond { get; set; }
    /// <summary>
    /// 允许排队等待的请求数
    /// </summary>
    int QueueLimit { get; set; }
}
