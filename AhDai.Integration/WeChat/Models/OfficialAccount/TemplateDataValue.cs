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
    public string Value { get; init; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="value">模板字段值</param>
    public TemplateDataValue(string value)
    {
        Value = value;
    }
}