namespace AhDai.Integration.Tianyancha.Models;

/// <summary>
/// TianyanchaTaxpayerOutput
/// </summary>
public class TaxpayerOutput
{
    /// <summary>
    /// Id
    /// </summary>
    public long Gid { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 简称
    /// </summary>
    public string Alias { get; set; } = default!;
    /// <summary>
    /// Logo
    /// </summary>
    public string Logo { get; set; } = default!;
    /// <summary>
    /// 纳税人类型
    /// </summary>
    public string TaxpayerQualificationType { get; set; } = default!;
    /// <summary>
    /// 纳税人识别号
    /// </summary>
    public string TaxpayerIdentificationNumber { get; set; } = default!;
    /// <summary>
    /// 开始日期
    /// </summary>
    public string? StartDate { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public string? EndDate { get; set; }

}
