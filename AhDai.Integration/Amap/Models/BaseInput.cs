using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

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
    /// 数字签名
    /// </summary>
    [JsonPropertyName("sig")]
    public virtual string? Sig { get; set; }
    /// <summary>
    /// 返回数据格式类型
    /// 可选输入内容包括：JSON，XML。设置 JSON 返回结果数据将会以 JSON 结构构成；如果设置 XML 返回结果数据将以 XML 结构构成。
    /// </summary>
    [JsonPropertyName("output")]
    public virtual string? Output { get; set; }
    /// <summary>
    /// 回调函数
    /// callback 值是用户定义的函数名称，此参数只在 output 参数设置为 JSON 时有效。
    /// </summary>
    [JsonPropertyName("callback")]
    public virtual string? Callback { get; set; }
}
