namespace AhDai.Core.Configs;

/// <summary>
/// RedisConfig
/// </summary>
public class RedisConfig
{
    /// <summary>
    /// 主机
    /// </summary>
    public string Host { get; set; } = default!;
    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; } 
    /// <summary>
    /// 连接失败时放弃连接
    /// 如果为 true，则 Connect 不会在没有可用服务器时创建连接
    /// </summary>
    public bool AbortConnect { get; set; }
    /// <summary>
    /// 默认库
    /// </summary>
    public int Database { get; set; }
}
