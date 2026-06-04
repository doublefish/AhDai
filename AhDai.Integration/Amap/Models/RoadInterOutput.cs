using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 道路交叉口
/// </summary>
public class RoadInterOutput
{
    /// <summary>
    /// 坐标点
    /// </summary>
    [JsonPropertyName("location")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Location { get; set; } = default!;
    /// <summary>
    /// 方位，输入点相对路口的方位
    /// </summary>
    [JsonPropertyName("direction")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Direction { get; set; } = default!;
    /// <summary>
    /// 交叉路口到请求坐标的距离，单位：米
    /// </summary>
    [JsonPropertyName("distance")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Distance { get; set; } = default!;
    /// <summary>
    /// 第一条道路Id
    /// </summary>
    [JsonPropertyName("first_id")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string FirstId { get; set; } = default!;
    /// <summary>
    /// 第一条道路名称
    /// </summary>
    [JsonPropertyName("first_name")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string FirstName { get; set; } = default!;
    /// <summary>
    /// 第二条道路Id
    /// </summary>
    [JsonPropertyName("second_id")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string SecondId { get; set; } = default!;
    /// <summary>
    /// 第二条道路名称
    /// </summary>
    [JsonPropertyName("second_name")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string SecondName { get; set; } = default!;
}
