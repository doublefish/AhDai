using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 身份证信息提交
/// </summary>
public class IdCardSumbitInput
{
    /// <summary>
    /// VerifyToken
    /// </summary>
    [Required]
    [JsonPropertyName("verify_token")]
    public string VerifyToken { get; set; } = default!;
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    [JsonPropertyName("id_name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 身份证号
    /// </summary>
    [Required]
    [JsonPropertyName("id_no")]
    public string No { get; set; } = default!;
    /// <summary>
    /// 证件类型：0 大陆居民二代身份证 4 港澳台居民居住证
    /// </summary>
    [JsonPropertyName("certificate_type")]
    public int CertificateType { get; set; }
}
