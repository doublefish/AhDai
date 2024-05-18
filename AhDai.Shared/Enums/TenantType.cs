using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 租户类型
/// </summary>
public enum TenantType
{
	/// <summary>
	/// 普通
	/// </summary>
	[Display(Name = "普通")]
	Normal = 0,
	/// <summary>
	/// 系统
	/// </summary>
	[Display(Name = "系统")]
	System = 1,
}
