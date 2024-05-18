using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 菜单类型
/// </summary>
public enum MenuType
{
	/// <summary>
	/// 目录
	/// </summary>
	[Display(Name = "目录")]
	Directory = 0,
	/// <summary>
	/// 页面
	/// </summary>
	[Display(Name = "页面")]
	Page = 1,
	/// <summary>
	/// 按钮
	/// </summary>
	[Display(Name = "按钮")]
	Button = 2,
}
