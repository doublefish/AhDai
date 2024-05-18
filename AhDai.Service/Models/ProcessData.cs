using System.Linq;
using System.Text.Json.Serialization;

namespace AhDai.Service.Models;

/// <summary>
/// 流程数据
/// </summary>
public class ProcessData
{
	/// <summary>
	/// 流程实例Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 受让人
	/// </summary>
	[JsonIgnore]
	public long[] AssigneeIds { get; set; } = default!;
    /// <summary>
    /// 审批状态
    /// </summary>
    public Shared.Enums.WorkflowApprovalStatus Status { get; set; }
	/// <summary>
	/// 审批状态名称
	/// </summary>
	public string? StatusName => Status.GetDisplayName();
	/// <summary>
	/// 是否已开始流程
	/// </summary>
	public bool IsStarted => Status != Shared.Enums.WorkflowApprovalStatus.Unsubmitted;
	/// <summary>
	/// 是否运行中
	/// </summary>
	public bool IsRuning => Status == Shared.Enums.WorkflowApprovalStatus.Pending || Status == Shared.Enums.WorkflowApprovalStatus.Suspended;
	/// <summary>
	/// 是否可以启动流程
	/// </summary>
	public bool CanStart => Id == 0 && CreatorId == UserId;
	/// <summary>
	/// 是否可以重启流程
	/// </summary>
	public bool CanRestart => Status == Shared.Enums.WorkflowApprovalStatus.Cancelled || Status == Shared.Enums.WorkflowApprovalStatus.Rejected;
	/// <summary>
	/// 是否可以撤销流程
	/// </summary>
	public bool CanCancel => IsRuning && CreatorId == UserId;
	/// <summary>
	/// 是否可以审批流程
	/// </summary>
	public bool CanApproval => IsRuning && AssigneeIds != null && AssigneeIds.Contains(UserId);

	/// <summary>
	/// DataId
	/// </summary>
	[JsonIgnore]
	public string DataTable { get; set; } = "";
	/// <summary>
	/// DataId
	/// </summary>
	[JsonIgnore]
	public long DataId { get; set; }
	/// <summary>
	/// CreatorId
	/// </summary>
	[JsonIgnore]
	public long CreatorId { get; set; }
	/// <summary>
	/// 当前用户Id
	/// </summary>
	[JsonIgnore]
	public long UserId { get; set; }


}
