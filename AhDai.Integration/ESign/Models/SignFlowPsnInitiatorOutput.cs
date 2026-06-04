using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人发起方信息
/// </summary>
public class SignFlowPsnInitiatorOutput
{
    /// <summary>
    /// 个人发起方账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string? PsnId { get; set; }
    /// <summary>
    /// 个人发起方姓名
    /// </summary>
    [JsonPropertyName("psnName")]
    public string? PsnName { get; set; }
}
