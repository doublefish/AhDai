using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 流程审批人类型
/// </summary>
public enum WorkflowApproverType
{
	/// <summary>
	/// 部门领导
	/// </summary>
	[Display(Name = "部门领导")]
	DepartmentLeader = 1,
	/// <summary>
	/// 指定人员
	/// </summary>
	[Display(Name = "指定人员")]
	Assigned = 2,
	/// <summary>
	/// 业务经理
	/// </summary>
	[Display(Name = "业务经理")]
	BusinessManager = 31,
	/// <summary>
	/// 客服经理
	/// </summary>
	[Display(Name = "客服经理")]
	CustomerServiceManager = 32,
	/// <summary>
	/// 成本专员
	/// </summary>
	[Display(Name = "成本专员")]
	CostSpecialist = 33,
	/// <summary>
	/// 标书专员
	/// </summary>
	[Display(Name = "标书专员")]
	BidSpecialist = 34,
	/// <summary>
	/// 会计专员
	/// </summary>
	[Display(Name = "会计专员")]
	AccountingSpecialist = 35,
}
