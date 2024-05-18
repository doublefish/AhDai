using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 通用状态
/// </summary>
public enum GenericStatus
{
	/// <summary>
	/// 启用
	/// </summary>
	[Display(Name = "启用")]
	Enabled = 1,
	/// <summary>
	/// 禁用
	/// </summary>
	[Display(Name = "禁用")]
	Disabled = 2,
}
