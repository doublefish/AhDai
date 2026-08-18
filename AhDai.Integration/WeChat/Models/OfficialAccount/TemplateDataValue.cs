using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.OfficialAccount;

/// <summary>
/// 模板数据
/// </summary>
public record TemplateDataValue
{
    /// <summary>
    /// 模板字段值
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
