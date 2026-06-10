using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 授权码
/// </summary>
public class AuthCodeOutput
{
    /// <summary>
    /// AppKey
    /// </summary>
    [JsonPropertyName("appKey")]
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// 重定向回调地址
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    public string RedirectUrl { get; set; } = default!;
    /// <summary>
    /// 授权码
    /// </summary>
    [JsonPropertyName("authCode")]
    public string AuthCode { get; set; } = default!;
}
