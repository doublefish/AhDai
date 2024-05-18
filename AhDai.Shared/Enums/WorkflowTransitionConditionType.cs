using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程流转条件类型
/// </summary>
public enum WorkflowTransitionConditionType
{
	/// <summary>
	/// 审批结果
	/// </summary>
	[Display(Name = "审批结果")]
	ApprovalResult = 1,
	/// <summary>
	/// 表单字段
	/// </summary>
	[Display(Name = "表单字段")]
	FormField = 2,
}
