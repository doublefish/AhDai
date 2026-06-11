using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 刷新用户授权凭证
/// </summary>
public class RefreshUserAccessTokenInput
{
    /// <summary>
    /// 用户访问凭证
    /// </summary>
    [Required]
    [JsonPropertyName("userAccessToken")]
    public string UserAccessToken { get; init; } = default!;
    /// <summary>
    /// 刷新用户访问凭证
    /// </summary>
    [Required]
    [JsonPropertyName("refreshUserToken")]
    public string RefreshUserToken { get; init; } = default!;
}
