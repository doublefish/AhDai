using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人签署方信息
/// </summary>
public class PersonSignerOutput
{
    /// <summary>
    /// 个人账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string PsnId { get; set; } = default!;
    /// <summary>
    /// 个人姓名
    /// </summary>
    [JsonPropertyName("psnName")]
    public string PsnName { get; set; } = default!;
    /// <summary>
    /// 个人账号标识（手机号/邮箱）
    /// </summary>
    [JsonPropertyName("psnAccount")]
    public PersonAccountOutput PsnAccount { get; set; } = default!;
}
