using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 获取用户访问凭证
/// </summary>
public class UserAccessTokenInput
{
    /// <summary>
    /// 授权码
    /// </summary>
    [Required]
    [JsonPropertyName("authCode")]
    public string AuthCode { get; init; } = default!;
}
