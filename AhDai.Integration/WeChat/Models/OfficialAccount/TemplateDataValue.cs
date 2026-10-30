using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.OfficialAccount;

/// <summary>
/// 模板数据
/// </summary>
/// <param name="Value"></param>
public record TemplateDataValue(string Value)
{
    /// <summary>
    /// 模板字段值
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; init; } = Value;
}