using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 性别
/// </summary>
public enum Gender
{
	/// <summary>
	/// 未知
	/// </summary>
	[Display(Name = "未知")]
	Unknown = 0,
	/// <summary>
	/// 男
	/// </summary>
	[Display(Name = "男")]
	Male = 1,
	/// <summary>
	/// 女
	/// </summary>
	[Display(Name = "女")]
	Female = 2,
}
