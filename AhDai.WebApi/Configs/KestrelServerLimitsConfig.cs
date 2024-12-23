namespace AhDai.WebApi.Configs;

/// <summary>
/// 服务限制配置
/// </summary>
public class KestrelServerLimitsConfig
{
    /// <summary>
    /// 请求体最大限制
    /// </summary>
    public long? MaxRequestBodySize { get; set; }
}
