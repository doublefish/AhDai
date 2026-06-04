using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 逆地理编码
/// <see href="https://baidumap.apifox.cn/api-32790722">详细文档请参阅</see>
/// </summary>
public class ReverseGeocodingInput : BaseMapInput
{
    /// <summary>
    /// 根据经纬度坐标获取地址。
    /// 示例：31.225696563611,121.49884033194
    /// </summary>
    [Required]
    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;
    /// <summary>
    /// 坐标的类型，目前支持的坐标类型包括：bd09ll（百度经纬度坐标）、bd09mc（百度米制坐标）、gcj02ll（国测局经纬度坐标，仅限中国）、wgs84ll（ GPS经纬度）
    /// 坐标系说明
    /// </summary>
    [JsonPropertyName("coordtype")]
    public string? Coordtype { get; set; }
    /// <summary>
    /// 可选参数，添加后返回国测局经纬度坐标或百度米制坐标 坐标系说明
    /// </summary>
    [JsonPropertyName("ret_coordtype")]
    public string? RetCoordtype { get; set; }
    /// <summary>
    /// poi召回半径，允许设置区间为0-1000米，超过1000米按1000米召回。
    /// </summary>
    [JsonPropertyName("radius")]
    public int? Radius { get; set; }
    /// <summary>
    /// 输出格式为json或者xml
    /// </summary>
    [JsonPropertyName("output")]
    public string? Output { get; set; }
    /// <summary>
    /// 将json格式的返回值通过callback函数返回以实现jsonp功能
    /// </summary>
    [JsonPropertyName("callback")]
    public string? Callback { get; set; }
    /// <summary>
    /// 可以选择poi类型召回不同类型的poi，例如poi_types=酒店，如想召回多个POI类型数据，可以‘|’分割 例如poi_types=酒店|房地产
    /// 不添加该参数则默认召回全部POI分类数据。poi分类
    /// </summary>
    [JsonPropertyName("poi_types")]
    public string? PoiTypes { get; set; }
    /// <summary>
    /// extensions_poi=0，不召回pois数据。
    /// extensions_poi=1，返回pois数据（默认显示周边1000米内的poi），并返回sematic_description语义化数据。
    /// </summary>
    [JsonPropertyName("extensions_poi")]
    public string? ExtensionsPoi { get; set; }
    /// <summary>
    /// 当取值为true时，召回坐标周围最近的3条道路数据。区别于行政区划中的street参数（street参数为行政区划中的街道，和普通道路不对应）。
    /// </summary>
    [JsonPropertyName("extensions_road")]
    public string? ExtensionsRoad { get; set; }
    /// <summary>
    /// 当取值为true时，行政区划返回乡镇级数据（town），仅国内召回乡镇数据。默认不访问。
    /// </summary>
    [JsonPropertyName("extensions_town")]
    public string? ExtensionsTown { get; set; }
    /// <summary>
    /// 指定召回的行政区划语言类型。 召回行政区划语言list（全量支持的语言见示例）。
    /// 当language=local时，根据请求中坐标所对应国家的母语类型，自动选择对应语言类型的行政区划召回。
    /// 目前支持多语言的行政区划区划包含country、province、city、district
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }
    /// <summary>
    /// 当用户指定language参数时，是否自动填充行政区划。 1填充，0不填充。
    /// 填充：当服务按某种语言类别召回时，若某一行政区划层级的语言数据未覆盖，则按照“英文→中文→本地语言”类别行政区划数据对该层级行政区划进行填充，保证行政区划数据召回完整性。
    /// </summary>
    [JsonPropertyName("language_auto")]
    public int? LanguageAuto { get; set; }

}
