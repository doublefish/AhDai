namespace AhDai.Service;

/// <summary>
/// 流程数据出参
/// </summary>
public interface IProcessOutput : IBaseOutput
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public Shared.Enums.WorkflowApprovalStatus ApprovalStatus { get; set; }
    /// <summary>
    /// 审批状态名称
    /// </summary>
    public string? ApprovalStatusName { get; set; }
    /// <summary>
    /// 流程信息
    /// </summary>
    public Models.ProcessData? Process { get; set; }

}
