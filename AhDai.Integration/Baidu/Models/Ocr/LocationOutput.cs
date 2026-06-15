using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// LocationOutput
/// </summary>
public class LocationOutput
{
    /// <summary>
    /// 表示定位位置的长方形左上顶点的水平坐标
    /// </summary>
    [JsonPropertyName("left")]
    public uint? Left { get; set; }
    /// <summary>
    /// 表示定位位置的长方形左上顶点的垂直坐标
    /// </summary>
    [JsonPropertyName("top")]
    public uint? Top { get; set; }
    /// <summary>
    /// 表示定位位置的长方形的宽度
    /// </summary>
    [JsonPropertyName("width")]
    public uint? Width { get; set; }
    /// <summary>
    /// 表示定位位置的长方形的高度
    /// </summary>
    [JsonPropertyName("height")]
    public uint? Height { get; set; }
}
