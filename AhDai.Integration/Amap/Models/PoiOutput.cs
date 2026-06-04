using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// Poi
/// </summary>
public class PoiOutput
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
    /// 类型
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Type { get; set; } = default!;
    /// <summary>
    /// 电话
    /// </summary>
    [JsonPropertyName("tel")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Tel { get; set; } = default!;
    /// <summary>
    /// 地址
    /// </summary>
    [JsonPropertyName("address")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 所在商圈名称
    /// </summary>
    [JsonPropertyName("businessarea")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string BusinessArea { get; set; } = default!;
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
    /// 中心点到请求坐标的距离，单位：米
    /// </summary>
    [JsonPropertyName("distance")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Distance { get; set; } = default!;
}
