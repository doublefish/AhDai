using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程任务类型
/// </summary>
public enum WorkflowTaskType
{
	/// <summary>
	/// 普通
	/// </summary>
	[Display(Name = "普通")]
	Normal = 0,
	/// <summary>
	/// 转办
	/// </summary>
	[Display(Name = "转办")]
	Forward = 1,
}
