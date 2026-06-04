using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人发起方信息
/// </summary>
public class SignFlowPsnInitiatorInput
{
    /// <summary>
    /// 个人发起方账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string? PsnId { get; set; }
}
