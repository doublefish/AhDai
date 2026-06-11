using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 非设备操作token
/// </summary>
public class OpsTokenOutput
{
    /// <summary>
    /// token过期时间到，时间戳，精确到秒
    /// </summary>
    [JsonPropertyName("tokenExpire")]
    public int TokenExpire { get; set; }
    /// <summary>
    /// 非设备操作的token
    /// </summary>
    [JsonPropertyName("httpUrlToken")]
    public string HttpUrlToken { get; set; } = default!;
    /// <summary>
    /// token对应的appKey
    /// </summary>
    [JsonPropertyName("tokenAppKey")]
    public string TokenAppKey { get; set; } = default!;
}
