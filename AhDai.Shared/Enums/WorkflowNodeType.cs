using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程节点类型
/// </summary>
public enum WorkflowNodeType
{
	/// <summary>
	/// 开始
	/// </summary>
	[Display(Name = "开始")]
	Start = 1,
	/// <summary>
	/// 结束
	/// </summary>
	[Display(Name = "结束")]
	End = 2,
	/// <summary>
	/// 活动
	/// </summary>
	[Display(Name = "活动")]
	Activity = 3,
	/// <summary>
	/// 决策
	/// </summary>
	[Display(Name = "决策")]
	Decision = 4,
	/// <summary>
	/// 并行
	/// </summary>
	[Display(Name = "并行")]
	Parallel = 5,
	/// <summary>
	/// 合并
	/// </summary>
	[Display(Name = "合并")]
	Merge = 6,
	/// <summary>
	/// 子流程
	/// </summary>
	[Display(Name = "子流程")]
	Subprocess = 7,
}
