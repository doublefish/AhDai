using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 签署流程通知
/// </summary>
public class SignFlowNotifyOutput
{
    /// <summary>
    /// 标记该通知的业务类型，该通知固定为：SIGN_FLOW_COMPLETE
    /// </summary>
    [JsonPropertyName("action")]
    public string Action { get; set; } = default!;
    /// <summary>
    /// 回调通知触发时间（如重试多次均返回第一次时间，毫秒级时间戳格式）
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
    /// <summary>
    /// 签署流程ID
    /// </summary>
    [JsonPropertyName("signFlowId")]
    public string SignFlowId { get; set; } = default!;
    /// <summary>
    /// 签署流程标题
    /// </summary>
    [JsonPropertyName("signFlowTitle")]
    public string SignFlowTitle { get; set; } = default!;
    /// <summary>
    /// 签署流程最终状态
    /// 2 - 已完成（所有签署方完成签署）
    /// 3 - 已撤销（发起方撤销签署任务）
    /// 5 - 已过期（签署截止日到期后触发）
    /// 7 - 已拒签（签署方拒绝签署）
    /// </summary>
    [JsonPropertyName("signFlowStatus")]
    public string SignFlowStatus { get; set; } = default!;
    /// <summary>
    /// 当流程非签署完成，其他原因结束时，附加原因描述
    /// </summary>
    [JsonPropertyName("statusDescription")]
    public string StatusDescription { get; set; } = default!;
    /// <summary>
    /// 签署流程创建时间（Unix时间戳格式，单位：毫秒）
    /// </summary>
    [JsonPropertyName("signFlowCreateTime")]
    public long SignFlowCreateTime { get; set; }
    /// <summary>
    /// 签署流程开启时间（Unix时间戳格式，单位：毫秒）
    /// </summary>
    [JsonPropertyName("signFlowStartTime")]
    public long SignFlowStartTime { get; set; }
    /// <summary>
    /// 签署流程完结时间（Unix时间戳格式，单位：毫秒）
    /// </summary>
    [JsonPropertyName("signFlowFinishTime")]
    public long SignFlowFinishTime { get; set; }

    /// <summary>
    /// 是否已完成
    /// </summary>
    public bool IsCompleted => Action == "SIGN_FLOW_COMPLETE" && SignFlowStatus == "2";
}
