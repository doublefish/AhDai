using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人账号
/// </summary>
public class PersonAccountOutput
{
    /// <summary>
    /// 用户登录e签宝SaaS官网的手机号
    /// </summary>
    [JsonPropertyName("accountMobile")]
    public string? AccountMobile { get; set; }
    /// <summary>
    /// 用户登录e签宝SaaS官网的邮箱地址
    /// </summary>
    [JsonPropertyName("accountEmail")]
    public string? AccountEmail { get; set; }
}
