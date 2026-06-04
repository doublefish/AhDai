using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 归属区域面
/// </summary>
public class PoiRegionOutput
{
    /// <summary>
    /// 请求中的坐标与所归属区域面的相对位置关系
    /// </summary>
    [JsonPropertyName("direction_desc")]
    public string DirectionDesc { get; set; } = default!;
    /// <summary>
    /// 归属区域面名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 归属区域面类型
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = default!;
}
