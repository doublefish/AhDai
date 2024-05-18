namespace AhDai.Entity;

/// <summary>
/// IProcessEntity
/// </summary>
public interface IProcessEntity
{
	/// <summary>
	/// 租户Id
	/// </summary>
	public long TenantId { get; set; }
	/// <summary>
	/// 审批状态
	/// </summary>
	public Shared.Enums.WorkflowApprovalStatus ApprovalStatus { get; set; }
}
