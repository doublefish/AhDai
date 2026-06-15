using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.OfficialAccount;

/// <summary>
/// 模板消息
/// </summary>
public class TemplateMessageInput
{
    /// <summary>
    /// 接收者openid
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
    /// 模板跳转链接（海外账号没有跳转能力）
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// 跳小程序所需数据，不需跳小程序可不用传该数据
    /// </summary>
    [JsonPropertyName("miniprogram")]
    public object? MiniProgram { get; set; }
    /// <summary>
    /// 模板数据：value=> { "data": "", "color": "" }
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("data")]
    public object Data { get; set; } = default!;
    /// <summary>
    /// 防重入id。对于同一个openid + client_msg_id, 只发送一条消息,10分钟有效,超过10分钟不保证效果。若无防重入需求，可不填
    /// </summary>
    [JsonPropertyName("client_msg_id")]
    public string? ClientMsgId { get; set; }
}
