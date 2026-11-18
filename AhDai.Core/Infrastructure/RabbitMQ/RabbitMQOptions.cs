namespace AhDai.Core.Infrastructure.RabbitMQ;

/// <summary>
/// RabbitMQOptions
/// </summary>
public class RabbitMQOptions
{
    /// <summary>
    /// 主机名
    /// </summary>
    public string HostName { get; set; } = "localhost";
    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; } = 5672;
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = "guest";
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = "guest";
    /// <summary>
    /// Virtual Host
    /// </summary>
    public string VirtualHost { get; set; } = "/";
    /// <summary>
    /// 客户端连接名称
    /// 用于 RabbitMQ 管理后台识别连接来源
    /// </summary>
    public string? ClientProvidedName { get; set; }
    /// <summary>
    /// 是否启用自动连接恢复
    /// </summary>
    public bool AutomaticRecoveryEnabled { get; set; } = true;
    /// <summary>
    /// 是否启用拓扑恢复
    /// </summary>
    public bool TopologyRecoveryEnabled { get; set; } = true;
    /// <summary>
    /// 网络连接恢复间隔（秒）
    /// </summary>
    public int NetworkRecoveryIntervalSeconds { get; set; } = 5;
}
