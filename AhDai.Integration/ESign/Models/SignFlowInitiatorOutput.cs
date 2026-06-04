using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起方信息
/// </summary>
public class SignFlowInitiatorOutput
{
    /// <summary>
    /// 发起人信息
    /// </summary>
    [JsonPropertyName("psnInitiator")]
    public SignFlowPsnInitiatorOutput? PsnInitiator { get; set; }
    /// <summary>
    /// 发起机构信息
    /// </summary>
    [JsonPropertyName("orgInitiator")]
    public SignFlowOrgInitiatorOutput? OrgInitiator { get; set; }
}
