using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 模板消息
/// </summary>
public class TemplateMessageOutput : BaseOutput
{
    /// <summary>
    /// 消息Id
    /// </summary>
    [JsonPropertyName("msgid")]
    public long? Id { get; set; }
}
