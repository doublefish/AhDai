using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构经办人信息
/// </summary>
public class SignFlowTransactorInput
{
    /// <summary>
    /// 经办人账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string? PsnId { get; set; }
}
