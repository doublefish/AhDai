using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构发起方信息
/// </summary>
public class SignFlowOrgInitiatorInput
{
    /// <summary>
    /// 机构发起方账号ID
    /// </summary>
    [JsonPropertyName("orgId")]
    public string? OrgId { get; set; }
    /// <summary>
    /// 机构发起方的经办人
    /// </summary>
    [JsonPropertyName("transactor")]
    public SignFlowTransactorInput? Transactor { get; set; }
}
