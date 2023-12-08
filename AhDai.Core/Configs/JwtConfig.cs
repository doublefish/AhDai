namespace AhDai.Core.Configs;

/// <summary>
/// JwtConfig
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// 签发人
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// 受众
    /// </summary>
    public string Audience { get; set; }
    /// <summary>
    /// 密钥
    /// </summary>
    public string SigningKey { get; set; }
    /// <summary>
    /// 超时时间（分钟）
    /// </summary>
    public int Expiration { get; set; }
    /// <summary>
    /// 允许服务器时间偏移量（秒）
    /// </summary>
    public int ClockSkew { get; set; }
		/// <summary>
		/// 启用Redis
		/// </summary>
		public bool EnableRedis { get; set; }
		/// <summary>
		/// RedisKey
		/// </summary>
		public string RedisKey { get; set; }
	}
