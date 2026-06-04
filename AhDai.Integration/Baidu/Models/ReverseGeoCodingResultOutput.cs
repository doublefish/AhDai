using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 逆地理编码
/// </summary>
public class ReverseGeoCodingResultOutput
{
    /// <summary>
    /// 经纬度坐标
    /// </summary>
    [JsonPropertyName("location")]
    public LocationOutput Location { get; set; } = default!;
    /// <summary>
    /// 结构化地址信息
    /// </summary>
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; } = default!;
    /// <summary>
    /// 国外行政区划，字段仅代表层级
    /// </summary>
    [JsonPropertyName("addressComponent")]
    public AddressComponentOutput AddressComponent { get; set; } = default!;
    /// <summary>
    /// 坐标所在商圈信息，如 "人民大学,中关村,苏州街"。最多返回3个。
    /// </summary>
    [JsonPropertyName("business")]
    public string Business { get; set; } = default!;
    /// <summary>
    /// 周边poi数组
    /// </summary>
    [JsonPropertyName("pois")]
    public PoiOutput[] Pois { get; set; } = default!;
    /// <summary>
    /// 周边道路
    /// </summary>
    [JsonPropertyName("roads")]
    public RoadOutput[] Roads { get; set; } = default!;
    /// <summary>
    /// 归属区域面
    /// </summary>
    [JsonPropertyName("poiRegions")]
    public PoiRegionOutput[] PoiRegions { get; set; } = default!;
    /// <summary>
    /// 当前位置结合POI的语义化结果描述。需设置extensions_poi=1才能返回。
    /// </summary>
    [JsonPropertyName("sematic_description")]
    public string SematicDescription { get; set; } = default!;
    /// <summary>
    /// 百度定义的城市id（正常更新与维护，但建议使用adcode）
    /// </summary>
    [JsonPropertyName("cityCode")]
    public int CityCode { get; set; }
}