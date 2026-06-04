using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 商圈
/// </summary>
public class BusinessAreaOutput
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
    /// 中心点坐标
    /// </summary>
    [JsonPropertyName("location")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Location { get; set; } = default!;
}
