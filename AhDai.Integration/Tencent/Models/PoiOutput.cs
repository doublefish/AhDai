using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 地点
/// </summary>
public class PoiOutput
{
    /// <summary>
    /// 地点（POI）唯一标识
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// 地址
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    /// <summary>
    /// 地点分类信息
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }
    /// <summary>
    /// 提示所述位置坐标
    /// </summary>
    [JsonPropertyName("location")]
    public LocationOutput? Location { get; set; }
    /// <summary>
    /// 行政区划信息
    /// </summary>
    [JsonPropertyName("ad_info")]
    public PoiAdInfoOutput? AdInfo { get; set; }
    /// <summary>
    /// 此参考位置到输入坐标的直线距离
    /// </summary>
    [JsonPropertyName("_distance")]
    public double? Distance { get; set; }
    /// <summary>
    /// 此参考位置到输入坐标的方位关系，如：北、南、内
    /// </summary>
    [JsonPropertyName("_dir_desc")]
    public string? DirDesc { get; set; }
    /// <summary>
    /// 附加字段：是否为aoi：1是（即该POI包含轮廓边界），0否
    /// </summary>
    [JsonPropertyName("is_aoi")]
    public int? IsAoi { get; set; }
    /// <summary>
    /// 附加字段：分类代码（仅policy=1/2/3时、及无poi分类筛选时支持）
    /// </summary>
    [JsonPropertyName("category_code")]
    public int? CategoryCode { get; set; }
    /// <summary>
    /// 附加字段：控制aoi返回面积，单位：平方米，非aoi返回-1
    /// </summary>
    [JsonPropertyName("area")]
    public double? Area { get; set; }
}
