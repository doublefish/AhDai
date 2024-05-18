using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Consts;

/// <summary>
/// 角色编码
/// </summary>
public class RoleCode
{
	/// <summary>
	/// 管理员
	/// </summary>
	[Display(Name = "管理员")]
	public const string Admin = "admin";
	/// <summary>
	/// 员工
	/// </summary>
	[Display(Name = "员工")]
	public const string Employee = "Employee";
	/// <summary>
	/// 人事
	/// </summary>
	[Display(Name = "HR")]
	public const string HR = "HR";
	/// <summary>
	/// 项目经理
	/// </summary>
	[Display(Name = "PM")]
	public const string PM = "PM";
}
