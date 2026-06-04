using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 银行回单文字识别
/// </summary>
public class OcrBankReceiptFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 回单编号
    /// </summary>
    public string? Number { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// 摘要
    /// </summary>
    public string? Summary { get; set; }
    /// <summary>
    /// 用途
    /// </summary>
    public string? Purpose { get; set; }
    /// <summary>
    /// 交易日期
    /// </summary>
    public DateOnly? Date { get; set; }
    /// <summary>
    /// 小写金额
    /// </summary>
    public decimal? Amount { get; set; }
    /// <summary>
    /// 大写金额
    /// </summary>
    public string? AmountInWords { get; set; }
    /// <summary>
    /// 付款人户名
    /// </summary>
    public string? PayerAccountName { get; set; }
    /// <summary>
    /// 付款人账号
    /// </summary>
    public string? PayerAccountNumber { get; set; }
    /// <summary>
    /// 付款人开户银行
    /// </summary>
    public string? PayerBankName { get; set; }
    /// <summary>
    /// 收款人户名
    /// </summary>
    public string? PayeeAccountName { get; set; }
    /// <summary>
    /// 收款人账号
    /// </summary>
    public string? PayeeAccountNumber { get; set; }
    /// <summary>
    /// 收款人开户银行
    /// </summary>
    public string? PayeeBankName { get; set; }
    /// <summary>
    /// 流水号
    /// </summary>
    public string? TransactionId { get; set; }
}
