using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程类型
/// </summary>
public enum WorkflowProcessType
{
	/// <summary>
	/// 通用
	/// </summary>
	[Display(Name = "通用")]
	Generic = 1,
	/// <summary>
	/// 定制
	/// </summary>
	[Display(Name = "定制")]
	Customized = 2,
}
