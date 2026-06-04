using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 签署页面链接
/// </summary>
public class SignFlowSignUrlOutput
{
    /// <summary>
    /// 签署短链接（有效期180天）
    /// </summary>
    [JsonPropertyName("shortUrl")]
    public string ShortUrl { get; set; } = default!;
    /// <summary>
    /// 签署长链接（永久有效）
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}
