using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 授权码
/// </summary>
public class AuthCodeInput
{
    /// <summary>
    /// AppKey
    /// </summary>
    [Required]
    [JsonPropertyName("appKey")]
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// 登录账号手机号
    /// </summary>
    [Required]
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = default!;
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;
    /// <summary>
    /// 重定向回调地址，需要在应用-安全设置中配置
    /// </summary>
    [Required]
    [JsonPropertyName("redirectUrl")]
    public string RedirectUrl { get; set; } = default!;
    /// <summary>
    /// 第三方业务参数, 重定向跳转时回传
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; set; } = default!;
}
