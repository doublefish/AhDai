using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AhDai.Integration.Aliyun.Models.Sms;

/// <summary>
/// SendInput
/// </summary>
public class SendInput
{
    /// <summary>
    /// 接收短信的手机号码
    /// </summary>
    [Required]
    public string PhoneNumbers { get; set; } = default!;
    /// <summary>
    /// 短信签名名称
    /// </summary>
    [Required]
    public string SignName { get; set; } = default!;
    /// <summary>
    /// 短信模板 Code
    /// </summary>
    [Required]
    public string TemplateCode { get; set; } = default!;
    /// <summary>
    /// 短信模板变量对应的实际值
    /// </summary>
    public IDictionary<string, string>? TemplateParam { get; set; }
    /// <summary>
    /// 上行短信扩展码
    /// </summary>
    public string? SmsUpExtendCode { get; set; }
    /// <summary>
    /// 外部流水扩展字段
    /// </summary>
    public string? OutId { get; set; }

}
