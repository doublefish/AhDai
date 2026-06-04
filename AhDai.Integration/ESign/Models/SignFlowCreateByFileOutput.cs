using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起签署流程
/// </summary>
public class SignFlowCreateByFileOutput
{
    /// <summary>
    /// 签署流程ID
    /// </summary>
    [JsonPropertyName("signFlowId")]
    public string? SignFlowId { get; set; }

}
