using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 催促签署
/// </summary>
public class SignFlowUrgeInput
{
    /// <summary>
    /// 通知方式（多种方式使用英文逗号分隔）
    /// 1- 短信，2 - 邮件 ，默认按照流程设置
    /// </summary>
    [JsonPropertyName("noticeTypes")]
    public string? NoticeTypes { get; set; }
    /// <summary>
    /// 指定被催签的签署人
    /// 为空表示：催签当前轮到签署但还未签署的所有签署人
    /// 按被催签人的账号标识或账号ID方式（二选一，根据发起签署时传入方式决定）
    /// </summary>
    [JsonPropertyName("urgedOperator")]
    public PersonAccountInput? UrgedOperator { get; set; }
}
