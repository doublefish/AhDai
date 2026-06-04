using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 社区信息
/// </summary>
public class NeighborhoodOutput
{
    /// <summary>
    /// 社区名称
    /// </summary>
    [JsonPropertyName("name")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Name { get; set; } = default!;
    /// <summary>
    /// POI 类型
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Type { get; set; } = default!;
}
