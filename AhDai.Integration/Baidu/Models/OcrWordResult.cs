using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrWordResult
/// </summary>
public class OcrWordResult
{
    /// <summary>
    /// 字段识别结果
    /// </summary>
    [JsonPropertyName("word")]
    public string Word { get; set; } = default!;
}
