using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models.Map;

/// <summary>
/// 行政区划信息
/// </summary>
public class GeocoderAdInfoOutput
{
    /// <summary>
    /// 国家代码（ISO3166标准3位数字码）
    /// </summary>
    [JsonPropertyName("nation_code")]
    public string NationCode { get; set; } = default!;
    /// <summary>
    /// 行政区划代码，规则详见：<see href="https://lbs.qq.com/service/webService/webServiceGuide/webServiceDistrict#6">行政区划代码说明</see>
    /// </summary>
    [JsonPropertyName("adcode")]
    public string AdCode { get; set; } = default!;
    /// <summary>
    /// 城市代码，由国家码+行政区划代码（提出城市级别）组合而来，总共为9位
    /// </summary>
    [JsonPropertyName("city_code")]
    public string CityCode { get; set; } = default!;
    /// <summary>
    /// 城市电话区号
    /// </summary>
    [JsonPropertyName("phone_area_code")]
    public string? PhoneAreaCode { get; set; }
    /// <summary>
    /// 行政区划名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 行政区划中心点坐标
    /// </summary>
    [JsonPropertyName("location")]
    public LocationOutput Location { get; set; } = default!;
    /// <summary>
    /// 国家
    /// </summary>
    [JsonPropertyName("nation")]
    public string Nation { get; set; } = default!;
    /// <summary>
    /// 省 / 直辖市
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 市 / 地级区 及同级行政区划，如果当前城市为省直辖县级区划，city与district字段均会返回此城市
    /// 注：省直辖县级区划adcode第3和第4位分别为9、0，如济源市adcode为419001
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = default!;
    /// <summary>
    /// 区 / 县级市 及同级行政区划
    /// </summary>
    [JsonPropertyName("district")]
    public string? District { get; set; }
    /// <summary>
    /// 此参考位置到输入坐标的直线距离
    /// </summary>
    [JsonPropertyName("_distance")]
    public double? Distance { get; set; }
}
