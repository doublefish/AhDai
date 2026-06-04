using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 模板控件
/// </summary>
public class DocTemplateComponentInput(string componentKey = "", string componentValue = "")
{
    /// <summary>
    /// 控件Key
    /// </summary>
    [JsonPropertyName("componentKey")]
    public string ComponentKey { get; set; } = componentKey;
    /// <summary>
    /// 控件填充值
    /// </summary>
    [JsonPropertyName("componentValue")]
    public string ComponentValue { get; set; } = componentValue;
}
