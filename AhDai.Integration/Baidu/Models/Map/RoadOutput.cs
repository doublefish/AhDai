using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// 周边道路
/// </summary>
public class RoadOutput
{
    /// <summary>
    /// 周边道路名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 传入的坐标点距离道路的大概距离
    /// </summary>
    [JsonPropertyName("distance")]
    public string Distance { get; set; } = default!;
}
