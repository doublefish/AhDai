using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AhDai.Integration.Aliyun.Models.Sms;

/// <summary>
/// SendInput
/// </summary>
public record SendInput
{
    /// <summary>
    /// 接收短信的手机号码
    /// </summary>
    [Required]
    public string PhoneNumbers { get; init; } = default!;
    /// <summary>
    /// 短信签名名称
    /// </summary>
    [Required]
    public string SignName { get; init; } = default!;
    /// <summary>
    /// 短信模板 Code
    /// </summary>
    [Required]
    public string TemplateCode { get; init; } = default!;
    /// <summary>
    /// 短信模板变量对应的实际值
    /// </summary>
    public IDictionary<string, string>? TemplateParam { get; init; }
    /// <summary>
    /// 上行短信扩展码
    /// </summary>
    public string? SmsUpExtendCode { get; init; }
    /// <summary>
    /// 外部流水扩展字段
    /// </summary>
    public string? OutId { get; init; }
}
