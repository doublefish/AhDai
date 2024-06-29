namespace AhDai.Core.Configs;

/// <summary>
/// ElasticConfig
/// </summary>
public class ElasticConfig
{
    /// <summary>
    /// 主机
    /// </summary>
    public string Host { get; set; } = default!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = default!;
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = default!;
    /// <summary>
    /// 默认索引
    /// </summary>
    public string DefaultIndex { get; set; } = default!;
}
