using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人账号
/// </summary>
public class PersonAccountInput
{
    /// <summary>
    /// 账号标识（手机号/邮箱）
    /// </summary>
    [JsonPropertyName("psnAccount")]
    public string? PsnAccount { get; set; }
    /// <summary>
    /// 账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string? PsnId { get; set; }
}
