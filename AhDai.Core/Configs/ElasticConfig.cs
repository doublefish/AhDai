namespace AhDai.Core.Configs;

/// <summary>
/// ElasticConfig
/// </summary>
public class ElasticConfig
{
    /// <summary>
    /// 主机
    /// </summary>
    public string Host { get; set; } = null!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = null!;
    /// <summary>
    /// 默认索引
    /// </summary>
    public string DefaultIndex { get; set; } = null!;
}
