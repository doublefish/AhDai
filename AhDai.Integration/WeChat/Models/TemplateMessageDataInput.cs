using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 模板消息数据
/// </summary>
public class TemplateMessageDataInput
{
    /// <summary>
    /// 模板数据
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("data")]
    public string Data { get; set; } = default!;
}
