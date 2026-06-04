using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// Aoi
/// </summary>
public class AoiOutput
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
    /// 所在区域编码
    /// </summary>
    [JsonPropertyName("adcode")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Adcode { get; set; } = default!;
    /// <summary>
    /// 所属 aoi 点面积，单位：平方米
    /// </summary>
    [JsonPropertyName("area")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Area { get; set; } = default!;
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
}
