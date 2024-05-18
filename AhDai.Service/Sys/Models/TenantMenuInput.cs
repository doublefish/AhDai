using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户菜单
/// </summary>
public class TenantMenuInput : BaseInput
{
	/// <summary>
	/// 菜单Id
	/// </summary>
	[Required]
	public long[] MenuIds { get; set; } = default!;
}
