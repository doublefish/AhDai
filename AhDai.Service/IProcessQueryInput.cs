namespace AhDai.Service;

/// <summary>
/// 流程数据查询入参
/// </summary>
public interface IProcessQueryInput : IBaseQueryInput
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public Shared.Enums.WorkflowApprovalStatus? ApprovalStatus { get; set; }
}
