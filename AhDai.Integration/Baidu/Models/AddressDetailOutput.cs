using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 地址明细
/// </summary>
public class AddressDetailOutput
{
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
    /// 百度城市代码
    /// </summary>
    [JsonPropertyName("city_code")]
    public int CityCode { get; set; }
    /// <summary>
    /// 区县名
    /// </summary>
    [JsonPropertyName("district")]
    public string District { get; set; } = default!;
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
}
