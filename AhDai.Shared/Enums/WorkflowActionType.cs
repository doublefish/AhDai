using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程操作类型
/// </summary>
public enum WorkflowActionType
{
	/// <summary>
	/// 发起
	/// </summary>
	[Display(Name = "发起")]
	Start = 1,
	/// <summary>
	/// 撤销
	/// </summary>
	[Display(Name = "撤销")]
	Cancel = 2,
	/// <summary>
	/// 批准
	/// </summary>
	[Display(Name = "批准")]
	Approve = 11,
	/// <summary>
	/// 拒绝
	/// </summary>
	[Display(Name = "拒绝")]
	Reject = 12,
	/// <summary>
	/// 转办
	/// </summary>
	[Display(Name = "转办")]
	Forward = 13,
}
