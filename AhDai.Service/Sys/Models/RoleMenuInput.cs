using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 角色菜单
/// </summary>
public class RoleMenuInput : BaseInput
{
	/// <summary>
	/// 菜单Id
	/// </summary>
	[Required]
	public long[] MenuIds { get; set; } = default!;
}
