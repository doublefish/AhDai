using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 开通账号
/// </summary>
public class OpenAccountInput
{
	/// <summary>
	/// 用户名
	/// </summary>
	[Required]
	public string Username { get; set; } = "";
	/// <summary>
	/// 手机号码
	/// </summary>
	[Required]
	public string MobilePhone { get; set; } = "";
}
