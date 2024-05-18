using System;

namespace AhDai.Service.Models;

/// <summary>
/// 银行账号
/// </summary>
public class BaseBankAccountInput : BaseInput
{
    /// <summary>
    /// 开户行名称
    /// </summary>
    public string BankName { get; set; } = "";
    /// <summary>
    /// 账号
    /// </summary>
    public string AccountNumber { get; set; } = "";
    /// <summary>
    /// 户名
    /// </summary>
    public string AccountName { get; set; } = "";
}
