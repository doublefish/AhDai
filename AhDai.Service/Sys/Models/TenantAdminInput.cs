using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户管理员
/// </summary>
public class TenantAdminInput : BaseInput
{
	/// <summary>
	/// 用户名
	/// </summary>
	[Required]
	public string Username { get; set; } = "";
	/// <summary>
	/// 姓名
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 邮箱
	/// </summary>
	public string? Email { get; set; } = "";
	/// <summary>
	/// 手机
	/// </summary>
	[Required]
	public string MobilePhone { get; set; } = "";
}
