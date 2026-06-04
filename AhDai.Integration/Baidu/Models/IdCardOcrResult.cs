using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 采集的身份证信息
/// </summary>
public class IdCardOcrResult
{
    /// <summary>
    /// 姓名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 身份证号
    /// </summary>
    [JsonPropertyName("id_card_number")]
    public string IdCardNumber { get; set; } = default!;
    /// <summary>
    /// 性别
    /// </summary>
    [JsonPropertyName("gender")]
    public string Gender { get; set; } = default!;
    /// <summary>
    /// 民族
    /// </summary>
    [JsonPropertyName("nation")]
    public string Nation { get; set; } = default!;
    /// <summary>
    /// 生日
    /// </summary>
    [JsonPropertyName("birthday")]
    public string Birthday { get; set; } = default!;
    /// <summary>
    /// 地址
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 签发机关
    /// </summary>
    [JsonPropertyName("issue_authority")]
    public string IssueAuthority { get; set; } = default!;
    /// <summary>
    /// 生效日期
    /// </summary>
    [JsonPropertyName("issue_time")]
    public string IssueTime { get; set; } = default!;
    /// <summary>
    /// 失效日期
    /// </summary>
    [JsonPropertyName("expire_time")]
    public string ExpireTime { get; set; } = default!;
}
