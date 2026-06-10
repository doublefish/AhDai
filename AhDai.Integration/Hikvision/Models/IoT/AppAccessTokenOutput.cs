using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 应用访问凭证
/// </summary>
public class AppAccessTokenOutput
{
    /// <summary>
    /// 应用唯一标识
    /// </summary>
    [JsonPropertyName("appKey")]
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// 应用访问凭证
    /// </summary>
    [JsonPropertyName("appAccessToken")]
    public string AppAccessToken { get; set; } = default!;
    /// <summary>
    /// 刷新token
    /// </summary>
    [JsonPropertyName("refreshAppToken")]
    public string RefreshAppToken { get; set; } = default!;
    /// <summary>
    /// 过期时间，单位：小时，最大有效期 7*24小时
    /// </summary>
    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; set; }
}
