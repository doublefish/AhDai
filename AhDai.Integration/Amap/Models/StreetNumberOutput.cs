using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 街道
/// </summary>
public class StreetNumberOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    [JsonPropertyName("street")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Street { get; set; } = default!;
    /// <summary>
    /// 门牌号
    /// </summary>
    [JsonPropertyName("number")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Number { get; set; } = default!;
    /// <summary>
    /// 坐标点
    /// </summary>
    [JsonPropertyName("location")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Location { get; set; } = default!;
    /// <summary>
    /// 方向，坐标点所处街道方位
    /// </summary>
    [JsonPropertyName("direction")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Direction { get; set; } = default!;
    /// <summary>
    /// 门牌地址到请求坐标的距离，单位：米
    /// </summary>
    [JsonPropertyName("distance")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Distance { get; set; } = default!;
}
