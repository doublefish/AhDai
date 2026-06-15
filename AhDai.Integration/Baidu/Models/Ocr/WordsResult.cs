using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// WordsResult
/// </summary>
public class WordsResult
{
    /// <summary>
    /// 字段识别结果
    /// </summary>
    [JsonPropertyName("words")]
    public string Words { get; set; } = default!;
}
