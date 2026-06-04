using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrWordsResult
/// </summary>
public class OcrWordsResult
{
    /// <summary>
    /// 字段识别结果
    /// </summary>
    [JsonPropertyName("words")]
    public string Words { get; set; } = default!;
}
