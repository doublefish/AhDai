using System.Collections;
using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起解约合同
/// </summary>
public class SignFlowRescissionInput
{
    /// <summary>
    /// 本次需要解约的签署文件ID列表（一次解约最多可添加10份文件）
    /// </summary>
    [JsonPropertyName("rescindFileList")]
    public string[]? RescindFileList { get; set; }
    /// <summary>
    /// 解约原因：传对应的数字枚举值
    /// 1 - 条款内容有误
    /// 2 - 印章选择错误
    /// 3 - 签署人信息错误
    /// 4 - 合作终止
    /// 5 - 其他
    /// </summary>
    [JsonPropertyName("rescindReason")]
    public string RescindReason { get; set; } = default!;
    /// <summary>
    /// 解约原因说明，最长200字
    /// </summary>
    [JsonPropertyName("rescindReasonNotes")]
    public string? RescindReasonNotes { get; set; }
    /// <summary>
    /// 合同解约发起方信息
    /// 仅原流程中的签署方或发起方可以发起解约；
    /// 发起方需先经过 用户授权（代个人/企业用户发起合同签署权限）；
    /// psnInitiator与orgInitiator二选一传入。
    /// </summary>
    [JsonPropertyName("rescissionInitiator")]
    public SignFlowInitiatorInput? RescissionInitiator { get; set; }
    /// <summary>
    /// 签署流程配置项
    /// </summary>
    [JsonPropertyName("signFlowConfig")]
    public object? SignFlowConfig { get; set; }
    /// <summary>
    /// 指定本次解约使用自动签署的机构签署方
    /// </summary>
    [JsonPropertyName("autoSignOrg")]
    public ArrayList? AutoSignOrg { get; set; }
    /// <summary>
    /// 指定本次解约机构签署方经办人信息
    /// </summary>
    [JsonPropertyName("orgSignerTransactor")]
    public ArrayList? OrgSignerTransactor { get; set; }

}
