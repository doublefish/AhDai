using System.Collections;
using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起方信息
/// </summary>
public class SignFlowInitiatorInput
{
    /// <summary>
    /// 发起人信息
    /// </summary>
    [JsonPropertyName("psnInitiator")]
    public SignFlowPsnInitiatorInput? PsnInitiator { get; set; }
    /// <summary>
    /// 发起机构信息
    /// </summary>
    [JsonPropertyName("orgInitiator")]
    public SignFlowOrgInitiatorInput? OrgInitiator { get; set; }
    /// <summary>
    /// 合同备注信息（显示到签署页面的任务详情信息里）
    /// </summary>
    [JsonPropertyName("initialRemarks")]
    public ArrayList? InitialRemarks { get; set; }
}
