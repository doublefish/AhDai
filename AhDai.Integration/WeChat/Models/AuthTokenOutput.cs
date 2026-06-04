using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// AuthTokenOutput
/// </summary>
public class AuthTokenOutput : BaseOutput
{
    /// <summary>
    /// 凭据
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// 用户刷新access_token
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
    /// <summary>
    /// 凭据有效时间，单位：秒，五分钟
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    /// <summary>
    /// OpenId
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; } = default!;
    /// <summary>
    /// 用户授权的作用域，使用逗号分隔
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = default!;
    /// <summary>
    /// UnionId，只有当scope为"snsapi_userinfo"时返回
    /// </summary>
    [JsonPropertyName("unionid")]
    public string? UnionId { get; set; }
}
