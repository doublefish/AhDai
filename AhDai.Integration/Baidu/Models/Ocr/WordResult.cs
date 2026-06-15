using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// WordResult
/// </summary>
public class WordResult
{
    /// <summary>
    /// 字段识别结果
    /// </summary>
    [JsonPropertyName("word")]
    public string Word { get; set; } = default!;
}
