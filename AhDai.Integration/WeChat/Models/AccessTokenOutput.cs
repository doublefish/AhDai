using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// AccessTokenOutput
/// </summary>
public class AccessTokenOutput : BaseOutput
{
    /// <summary>
    /// 凭据
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// 凭据有效时间，单位：秒
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
