using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 国外行政区划，字段仅代表层级
/// </summary>
public class AddressComponentOutput
{
    /// <summary>
    /// 国家
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = default!;
    /// <summary>
    /// 国家编码
    /// </summary>
    [JsonPropertyName("country_code")]
    public int CountryCode { get; set; }
    /// <summary>
    /// 国家英文缩写（三位）
    /// </summary>
    [JsonPropertyName("country_code_iso")]
    public string CountryCodeIso { get; set; } = default!;
    /// <summary>
    /// 国家英文缩写（两位）
    /// </summary>
    [JsonPropertyName("country_code_iso2")]
    public string CountryCodeIso2 { get; set; } = default!;
    /// <summary>
    /// 省名
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 城市名
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = default!;
    /// <summary>
    /// 城市所在级别（仅国外有参考意义。国外行政区划与中国有差异，城市对应的层级不一定为『city』。country、province、city、district、town分别对应0-4级，若city_level=3，则district层级为该国家的city层级）
    /// </summary>
    [JsonPropertyName("city_level")]
    public int CityLevel { get; set; }
    /// <summary>
    /// 区县名
    /// </summary>
    [JsonPropertyName("district")]
    public string District { get; set; } = default!;
    /// <summary>
    /// 乡镇名，需设置extensions_town=true时才会返回
    /// </summary>
    [JsonPropertyName("town")]
    public string Town { get; set; } = default!;
    /// <summary>
    /// 乡镇id
    /// </summary>
    [JsonPropertyName("town_code")]
    public string TownCode { get; set; } = default!;
    /// <summary>
    /// 街道名（行政区划中的街道层级）
    /// </summary>
    [JsonPropertyName("street")]
    public string Street { get; set; } = default!;
    /// <summary>
    /// 街道门牌号
    /// </summary>
    [JsonPropertyName("street_number")]
    public string StreetNumber { get; set; } = default!;
    /// <summary>
    /// 行政区划代码，前往下载
    /// </summary>
    [JsonPropertyName("adcode")]
    public string Adcode { get; set; } = default!;
    /// <summary>
    /// 相对当前坐标点的方向，当有门牌号的时候返回数据
    /// </summary>
    [JsonPropertyName("direction")]
    public string Direction { get; set; } = default!;
    /// <summary>
    /// 相对当前坐标点的距离，当有门牌号的时候返回数据
    /// </summary>
    [JsonPropertyName("distance")]
    public string Distance { get; set; } = default!;
}
