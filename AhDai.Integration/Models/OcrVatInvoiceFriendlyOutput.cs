using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 增值税发票文字识别
/// </summary>
public class OcrVatInvoiceFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 发票号
    /// </summary>
    public string? Number { get; set; }
    /// <summary>
    /// 发票类型
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// 销售方名称
    /// </summary>
    public string? SellerName { get; set; }
    /// <summary>
    /// 销售方纳税人识别号
    /// </summary>
    public string? SellerTaxNumber { get; set; }
    /// <summary>
    /// 购方名称
    /// </summary>
    public string? BuyerName { get; set; }
    /// <summary>
    /// 购方纳税人识别号
    /// </summary>
    public string? BuyerTaxNumber { get; set; }
    /// <summary>
    /// 开票日期
    /// </summary>
    public DateOnly? Date { get; set; }
    /// <summary>
    /// 含税金额
    /// </summary>
    public decimal? Amount { get; set; }
    /// <summary>
    /// 税率
    /// </summary>
    public decimal? TaxRate { get; set; }
    /// <summary>
    /// 税费
    /// </summary>
    public decimal? Tax { get; set; }
    /// <summary>
    /// 不含税金额
    /// </summary>
    public decimal? AmountExcludingTax { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}
