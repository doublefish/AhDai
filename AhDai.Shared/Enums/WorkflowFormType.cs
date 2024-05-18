using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程表单类型
/// </summary>
public enum WorkflowFormType
{
	/// <summary>
	/// 系统
	/// </summary>
	[Display(Name = "系统")]
	System = 1,
	/// <summary>
	/// 自定义
	/// </summary>
	[Display(Name = "自定义")]
	Custom = 2,
}
