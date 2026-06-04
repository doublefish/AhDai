using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 楼信息
/// </summary>
public class BuildingOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    [JsonPropertyName("name")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 类型
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Type { get; set; } = default!;
}
