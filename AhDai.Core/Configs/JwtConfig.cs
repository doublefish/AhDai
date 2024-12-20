namespace AhDai.Core.Configs;

/// <summary>
/// JwtConfig
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// 签发人
    /// </summary>
    public string Issuer { get; set; } = null!;
    /// <summary>
    /// 受众
    /// </summary>
    public string Audience { get; set; } = null!;
    /// <summary>
    /// 私钥
    /// </summary>
    public string PrivateKey { get; set; } = null!;
    /// <summary>
    /// 公钥
    /// </summary>
    public string PublicKey { get; set; } = null!;
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
    public string RedisKey { get; set; } = null!;
}
