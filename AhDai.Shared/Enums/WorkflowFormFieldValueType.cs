using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程表单字段值类型
/// </summary>
public enum WorkflowFormFieldValueType
{
	/// <summary>
	/// 整数
	/// </summary>
	[Display(Name = "整数")]
	Integer = 1,
	/// <summary>
	/// 数字
	/// </summary>
	[Display(Name = "数字")]
	Number = 2,
	/// <summary>
	/// 布尔
	/// </summary>
	[Display(Name = "布尔")]
	Boolean = 3,
	/// <summary>
	/// 字符串
	/// </summary>
	[Display(Name = "字符串")]
	String = 4,
}
