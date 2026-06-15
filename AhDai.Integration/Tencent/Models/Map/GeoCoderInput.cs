using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models.Map;

/// <summary>
/// GeocoderInput
/// <see href="https://lbs.qq.com/service/webService/webServiceGuide/address/Gcoder">详细文档请参阅</see>
/// </summary>
public class GeocoderInput : BaseInput
{
    /// <summary>
    /// 经纬度（GCJ02坐标系），格式：location=lat/纬度,lng/经度
    /// </summary>
    [Required]
    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;
    /// <summary>
    /// 解析行政区划的吸附半径，如入参经纬度在近海海域（未在任何行政区划内），可设置此参数解析返回在该半么范围内最近的行政区划信息。
    /// 单位米，默认0，最大设置5000
    /// </summary>
    [JsonPropertyName("radius")]
    public int? Radius { get; set; }
    /// <summary>
    /// 是否返回周边地点（POI）列表，可选值：0 不返回(默认)，1 返回
    /// </summary>
    [JsonPropertyName("get_poi")]
    public string? GetPoi { get; set; }
    /// <summary>
    /// 【单个参数写法示例】：poi_options=address_format=short
    /// 【多个参数英文分号间隔，写法示例】：poi_options=address_format=short;radius=5000;policy=2;orderby=_distance
    /// </summary>
    [JsonPropertyName("poi_options")]
    public string? PoiOptions { get; set; }
}
