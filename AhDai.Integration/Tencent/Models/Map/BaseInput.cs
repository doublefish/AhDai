using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models.Map;

/// <summary>
/// BaseInput
/// </summary>
public abstract class BaseInput
{
    /// <summary>
    /// 开发密钥（Key）
    /// </summary>
    [Required]
    [JsonPropertyName("key")]
    public virtual string Key { get; set; } = default!;
    /// <summary>
    /// 返回格式：支持JSON/JSONP，默认JSON
    /// </summary>
    [JsonPropertyName("output")]
    public virtual string? Output { get; set; }
    /// <summary>
    /// JSONP方式回调函数
    /// </summary>
    [JsonPropertyName("callback")]
    public virtual string? Callback { get; set; }
}
