using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// RegeocodeOutput
/// </summary>
public class RegeocodeOutput
{
    /// <summary>
    /// 格式化地址
    /// </summary>
    [JsonPropertyName("formatted_address")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string FormattedAddress { get; set; } = default!;
    /// <summary>
    /// 以行政区划+道路+门牌号等信息组成的标准格式化地址
    /// </summary>
    [JsonPropertyName("addressComponent")]
    public AddressComponentOutput AddressComponent { get; set; } = default!;
    /// <summary>
    /// 道路信息列表
    /// 请求参数 extensions 为 all 时返回如下内容
    /// </summary>
    [JsonPropertyName("roads")]
    [JsonConverter(typeof(AmapArrayConverter<RoadOutput>))]
    public RoadOutput[]? Roads { get; set; }
    /// <summary>
    /// 道路交叉口列表
    /// 请求参数 extensions 为 all 时返回如下内容
    /// </summary>
    [JsonPropertyName("roadinters")]
    [JsonConverter(typeof(AmapArrayConverter<RoadInterOutput>))]
    public RoadInterOutput[]? RoadInters { get; set; }
    /// <summary>
    /// poi 信息列表
    /// 请求参数 extensions 为 all 时返回如下内容
    /// </summary>
    [JsonPropertyName("pois")]
    [JsonConverter(typeof(AmapArrayConverter<PoiOutput>))]
    public PoiOutput[]? Pois { get; set; }
    /// <summary>
    /// aoi 信息列表
    /// 请求参数 extensions 为 all 时返回如下内容
    /// </summary>
    [JsonPropertyName("aois")]
    [JsonConverter(typeof(AmapArrayConverter<AoiOutput>))]
    public AoiOutput[]? Aois { get; set; }
}
