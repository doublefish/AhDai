using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 注册入参
/// </summary>
public class SignupInput
{
	/// <summary>
	/// 用户名
	/// </summary>
	[Required]
	public string Username { get; set; }
	/// <summary>
	/// 密码
	/// </summary>
	[Required]
	public string Password { get; set; }
}
