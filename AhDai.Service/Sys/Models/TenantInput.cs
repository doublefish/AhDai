using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户
/// </summary>
public class TenantInput : BaseInput
{
	/// <summary>
	/// 名称
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 管理员：新增时有效
	/// </summary>
	public TenantAdminInput? Admin { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
}
