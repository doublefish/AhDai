using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// OrgAuthUrlOutput
/// </summary>
public class OrgAuthUrlOutput
{
    /// <summary>
    /// 机构认证授权长链接（有效期30天）
    /// </summary>
    [JsonPropertyName("authFlowId")]
    public string AuthFlowId { get; set; } = default!;
    /// <summary>
    /// 机构认证授权短链接 （有效期30天）
    /// </summary>
    [JsonPropertyName("authUrl")]
    public string AuthUrl { get; set; } = default!;
    /// <summary>
    /// 本次认证授权流程ID
    /// </summary>
    [JsonPropertyName("authShortUrl")]
    public string AuthShortUrl { get; set; } = default!;
}
