using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// SessionOutput
/// </summary>
public class SessionOutput : BaseOutput
{
    /// <summary>
    /// 会话密钥
    /// </summary>
    [JsonPropertyName("session_key")]
    public string SessionKey { get; set; } = default!;
    /// <summary>
    /// 用户唯一标识
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; } = default!;
    /// <summary>
    /// 用户在开放平台的唯一标识符
    /// </summary>
    [JsonPropertyName("unionid")]
    public string? UnionId { get; set; }
}
