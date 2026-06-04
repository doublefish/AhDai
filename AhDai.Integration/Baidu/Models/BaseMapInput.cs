using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseMapInput
/// </summary>
public abstract class BaseMapInput
{
    /// <summary>
    /// 开发者的访问密钥，必填项。v2之前该属性为key。 申请秘钥
    /// </summary>
    [Required]
    [JsonPropertyName("ak")]
    public virtual string Ak { get; set; } = default!;
    /// <summary>
    /// 可选，若开发者所用AK的校验方式为SN校验时该参数必须。
    /// </summary>
    [JsonPropertyName("sn")]
    public virtual string? Sn { get; set; }
}
