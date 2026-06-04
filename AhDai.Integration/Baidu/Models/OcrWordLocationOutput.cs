using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrWordLocationOutput
/// </summary>
public class OcrWordLocationOutput
{
    /// <summary>
    /// 字段的上边距
    /// </summary>
    [JsonPropertyName("top")]
    public uint Top { get; set; }
    /// <summary>
    /// 字段的左边距
    /// </summary>
    [JsonPropertyName("left")]
    public uint Left { get; set; }
    /// <summary>
    /// 字段的高度
    /// </summary>
    [JsonPropertyName("height")]
    public uint Height { get; set; }
    /// <summary>
    /// 字段的宽度
    /// </summary>
    [JsonPropertyName("width")]
    public uint Width { get; set; }
}
