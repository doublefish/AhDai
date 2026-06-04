using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// AccessTokenOutput
/// </summary>
public class AccessTokenOutput : BaseOutput
{
    /// <summary>
    /// AccessToken
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// 该参数忽略
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
    /// <summary>
    /// Access Token的有效期(秒为单位，有效期30天)
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    /// <summary>
    /// 该参数忽略
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = default!;
    /// <summary>
    /// 该参数忽略
    /// </summary>
    [JsonPropertyName("session_key")]
    public string SessionKey { get; set; } = default!;
    /// <summary>
    /// 该参数忽略
    /// </summary>
    [JsonPropertyName("session_secret")]
    public string SessionSecret { get; set; } = default!;
}
