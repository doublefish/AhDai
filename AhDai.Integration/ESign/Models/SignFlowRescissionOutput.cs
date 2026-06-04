using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起解约合同
/// </summary>
public class SignFlowRescissionOutput
{
    /// <summary>
    /// 签署流程ID
    /// </summary>
    [JsonPropertyName("signFlowId")]
    public string? SignFlowId { get; set; }

}
