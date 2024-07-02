namespace AhDai.Core.Configs;

/// <summary>
/// JwtConfig
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// 签发人
    /// </summary>
    public string Issuer { get; set; } = default!;
    /// <summary>
    /// 受众
    /// </summary>
    public string Audience { get; set; } = default!;
    /// <summary>
    /// 密钥
    /// </summary>
    public string SigningKey { get; set; } = default!;
    /// <summary>
    /// 超时时间（分钟）
    /// </summary>
    public int Expiration { get; set; }
    /// <summary>
    /// 允许服务器时间偏移量（秒）
    /// </summary>
    public int ClockSkew { get; set; }
    /// <summary>
    /// 启用Redis，声明数据里必须有 Username，且具有唯一性
    /// </summary>
    public bool EnableRedis { get; set; }
    /// <summary>
    /// RedisKey
    /// </summary>
    public string RedisKey { get; set; } = default!;
}
