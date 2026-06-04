using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构签署方信息
/// </summary>
public class OrgSignerOutput
{
    /// <summary>
    /// 机构账号ID
    /// </summary>
    [JsonPropertyName("orgId")]
    public string OrgId { get; set; } = default!;
    /// <summary>
    /// 机构名称
    /// </summary>
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = default!;
    /// <summary>
    /// 机构经办人
    /// </summary>
    [JsonPropertyName("transactor")]
    public PersonSignerOutput Transactor { get; set; } = default!;
}
