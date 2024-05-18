namespace AhDai.Service.Models;

/// <summary>
/// 银行账号
/// </summary>
/// <param name="bankName"></param>
/// <param name="accountNumber"></param>
/// <param name="accountName"></param>
public class BaseBankAccountOutput(string bankName = "", string accountNumber = "", string accountName = "") : BaseOutput
{
	/// <summary>
	/// 开户行名称
	/// </summary>
	public virtual string BankName { get; set; } = bankName;
	/// <summary>
	/// 账号
	/// </summary>
	public virtual string AccountNumber { get; set; } = accountNumber;
	/// <summary>
	/// 户名 （公司/个人名称）
	/// </summary>
	public virtual string AccountName { get; set; } = accountName;
}
