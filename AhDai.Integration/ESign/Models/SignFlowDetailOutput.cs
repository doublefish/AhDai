using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 签署流程详情
/// </summary>
public class SignFlowDetailOutput
{
    /// <summary>
    /// 签署流程Id
    /// </summary>
    public string SignFlowId { get; set; } = default!;
    /// <summary>
    /// 签署流程最终状态
    /// 0 - 草稿
    /// 1 - 签署中
    /// 2 - 已完成（所有签署方完成签署）
    /// 3 - 已撤销（发起方撤销签署任务）
    /// 5 - 已过期（签署截止日到期后触发）
    /// 7 - 已拒签（签署方拒绝签署）
    /// </summary>
    [JsonPropertyName("signFlowStatus")]
    public int SignFlowStatus { get; set; }
    /// <summary>
    /// 签署流程描述
    /// </summary>
    [JsonPropertyName("signFlowDescription")]
    public string? SignFlowDescription { get; set; }
    /// <summary>
    /// 签署流程的解约状态
    /// 0 - 未解约
    /// 1 - 解约中
    /// 2 - 部分解约
    /// 3 - 已解约
    /// </summary>
    [JsonPropertyName("rescissionStatus")]
    public int? RescissionStatus { get; set; }
    /// <summary>
    /// 对应的解约协议签署流程ID
    /// </summary>
    [JsonPropertyName("rescissionSignFlowIds")]
    public object? RescissionSignFlowIds { get; set; }
    /// <summary>
    /// 签署流程创建时间（毫秒级时间戳格式）
    /// </summary>
    [JsonPropertyName("signFlowCreateTime")]
    public long SignFlowCreateTime { get; set; }
    /// <summary>
    /// 签署流程开启时间（毫秒级时间戳格式）
    /// </summary>
    [JsonPropertyName("signFlowStartTime")]
    public long? SignFlowStartTime { get; set; }
    /// <summary>
    /// 签署流程结束时间（毫秒级时间戳格式）
    /// </summary>
    [JsonPropertyName("signFlowFinishTime")]
    public long? SignFlowFinishTime { get; set; }
    /// <summary>
    /// 签署流程发起方
    /// </summary>
    [JsonPropertyName("signFlowInitiator")]
    public SignFlowInitiatorOutput? SignFlowInitiator { get; set; }
    /// <summary>
    /// 签署流程配置项
    /// </summary>
    [JsonPropertyName("signFlowConfig")]
    public object? SignFlowConfig { get; set; }
    /// <summary>
    /// 签署文件信息
    /// </summary>
    [JsonPropertyName("docs")]
    public SignFlowDocOutput[]? Docs { get; set; }
    /// <summary>
    /// 附件信息
    /// </summary>
    [JsonPropertyName("attachments")]
    public object? Attachments { get; set; }
    /// <summary>
    /// 签署方信息
    /// </summary>
    [JsonPropertyName("signers")]
    public SignFlowSignerOutput[]? Signers { get; set; }
    /// <summary>
    /// 抄送方
    /// </summary>
    [JsonPropertyName("copiers")]
    public object? Copiers { get; set; }



}
