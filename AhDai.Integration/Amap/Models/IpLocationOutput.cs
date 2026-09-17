using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// IpLocationOutput
/// <see href="https://lbs.amap.com/api/webservice/guide/api/ipconfig">详细文档请参阅</see>
/// </summary>
public class IpLocationOutput : BaseOutput
{
    /// <summary>
    /// 省份名称
    /// </summary>
    [JsonPropertyName("province")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 城市名称
    /// </summary>
    [JsonPropertyName("city")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string City { get; set; } = default!;
    /// <summary>
    /// 城市的 adcode 编码
    /// </summary>
    [JsonPropertyName("adcode")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Adcode { get; set; } = default!;
    /// <summary>
    /// 所在城市矩形区域范围
    /// </summary>
    [JsonPropertyName("rectangle")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Rectangle { get; set; } = default!;
}
