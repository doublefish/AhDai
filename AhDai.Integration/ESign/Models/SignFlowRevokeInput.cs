using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 撤销签署流程
/// </summary>
public class SignFlowRevokeInput
{
    /// <summary>
    /// 撤销原因，最多50字
    /// </summary>
    [JsonPropertyName("revokeReason")]
    public string? RevokeReason { get; set; }
}
