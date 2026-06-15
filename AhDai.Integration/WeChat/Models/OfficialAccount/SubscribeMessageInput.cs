using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.OfficialAccount;

/// <summary>
/// 订阅消息
/// </summary>
public class SubscribeMessageInput
{
    /// <summary>
    /// 接收者（用户）的 openid
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("touser")]
    public string ToUser { get; set; } = default!;
    /// <summary>
    /// 模板ID
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = default!;
    /// <summary>
    /// 跳转网页时填写
    /// </summary>
    [JsonPropertyName("page")]
    public string? Page { get; set; }
    /// <summary>
    /// 跳转小程序时填写，格式如{ "appid": "pagepath": { "value": any } }
    /// </summary>
    [JsonPropertyName("miniprogram")]
    public object? MiniProgram { get; set; }
    /// <summary>
    /// 模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("data")]
    public object Data { get; set; } = default!;
}
