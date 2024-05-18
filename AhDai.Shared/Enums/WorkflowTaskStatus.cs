using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程任务状态
/// </summary>
public enum WorkflowTaskStatus
{
	/// <summary>
	/// 待办
	/// </summary>
	[Display(Name = "待办")]
	Pending = 0,
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
	/// 已完成
	/// </summary>
	[Display(Name = "已完成")]
	Completed = 11,
}
