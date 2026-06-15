using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// WordItemOutput
/// </summary>
public class WordItemOutput
{
    /// <summary>
    /// 行号
    /// </summary>
    [JsonPropertyName("row")]
    public int Row { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [JsonPropertyName("word")]
    public string Word { get; set; } = default!;
}
