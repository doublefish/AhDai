using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 驾驶证文字识别
/// </summary>
public class OcrDrivingLicenseFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 证号
    /// </summary>
    public string? Number { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public string? Sex { get; set; }
    /// <summary>
    /// 国籍
    /// </summary>
    public string? Nationality { get; set; }
    /// <summary>
    /// 住址
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateOnly? Birthday { get; set; }
    /// <summary>
    /// 准驾车型
    /// </summary>
    public string? Class { get; set; }
    /// <summary>
    /// 签发机关
    /// </summary>
    public string? IssuingAuthority { get; set; }
    /// <summary>
    /// 初次领证日期
    /// </summary>
    public DateOnly? FirstIssueDate { get; set; }
    /// <summary>
    /// 有效起始日期
    /// </summary>
    public DateOnly? IssueDate { get; set; }
    /// <summary>
    /// 有效截止日期
    /// </summary>
    public DateOnly? ExpiryDate { get; set; }
}
