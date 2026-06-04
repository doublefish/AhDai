using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 道路信息
/// </summary>
public class RoadOutput
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Id { get; set; } = default!;
    /// <summary>
    /// 名称
    /// </summary>
    [JsonPropertyName("name")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 坐标点
    /// </summary>
    [JsonPropertyName("location")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Location { get; set; } = default!;
    /// <summary>
    /// 方向，相对于输入点的方位
    /// </summary>
    [JsonPropertyName("direction")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Direction { get; set; } = default!;
    /// <summary>
    /// 到请求坐标的距离，单位：米
    /// </summary>
    [JsonPropertyName("distance")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Distance { get; set; } = default!;
}
