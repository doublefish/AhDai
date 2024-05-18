using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程审批状态
/// </summary>
public enum WorkflowApprovalStatus
{
	/// <summary>
	/// 未提交
	/// </summary>
	[Display(Name = "未提交")]
	Unsubmitted = 0,
	/// <summary>
	/// 待审批
	/// </summary>
	[Display(Name = "待审批")]
	Pending = 1,
	/// <summary>
	/// 已撤销
	/// </summary>
	[Display(Name = "已撤销")]
	Cancelled = 2,
	/// <summary>
	/// 已挂起
	/// </summary>
	[Display(Name = "已挂起")]
	Suspended = 10,
	/// <summary>
	/// 已批准
	/// </summary>
	[Display(Name = "已批准")]
	Approved = 11,
	/// <summary>
	/// 已拒绝
	/// </summary>
	[Display(Name = "已拒绝")]
	Rejected = 12,
}
