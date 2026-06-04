using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构经办人信息
/// </summary>
public class SignFlowTransactorOutput
{
    /// <summary>
    /// 经办人账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string? PsnId { get; set; }
    /// <summary>
    /// 经办人姓名
    /// </summary>
    [JsonPropertyName("psnName")]
    public string? PsnName { get; set; }
}
