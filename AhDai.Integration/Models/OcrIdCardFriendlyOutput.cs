using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 身份证文字识别
/// </summary>
public class OcrIdCardFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public string? Sex { get; set; }
    /// <summary>
    /// 民族
    /// </summary>
    public string? Nation { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateOnly? BirthDate { get; set; }
    /// <summary>
    /// 住址
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// 公民身份号码
    /// </summary>
    public string? IdNumber { get; set; }
    /// <summary>
    /// 签发机关
    /// </summary>
    public string? IssuingAuthority { get; set; }
    /// <summary>
    /// 签发日期
    /// </summary>
    public DateOnly? IssueDate { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateOnly? ExpirationDate { get; set; }
    /// <summary>
    /// 是否是长期有效
    /// </summary>
    public bool? IsLongTerm { get; set; }
}
