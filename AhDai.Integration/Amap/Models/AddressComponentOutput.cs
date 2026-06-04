using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// 地址元素
/// </summary>
public class AddressComponentOutput
{
    /// <summary>
    /// 坐标点所在国家名称
    /// </summary>
    [JsonPropertyName("country")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Country { get; set; } = default!;
    /// <summary>
    /// 坐标点所在省名称 
    /// </summary>
    [JsonPropertyName("province")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 坐标点所在城市名称
    /// 请注意：当城市是省直辖县时返回为空，以及城市为北京、上海、天津、重庆四个直辖市时，该字段返回为空；<see href="https://lbs.amap.com/faq/webservice/webservice-api/geocoding/43267">省直辖县列表</see>
    /// </summary>
    [JsonPropertyName("city")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string City { get; set; } = default!;
    /// <summary>
    /// 城市编码
    /// </summary>
    [JsonPropertyName("citycode")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Citycode { get; set; } = default!;
    /// <summary>
    /// 坐标点所在区
    /// </summary>
    [JsonPropertyName("district")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string District { get; set; } = default!;
    /// <summary>
    /// 行政区编码
    /// </summary>
    [JsonPropertyName("adcode")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Adcode { get; set; } = default!;
    /// <summary>
    /// 坐标点所在乡镇/街道（此街道为社区街道，不是道路信息）
    /// </summary>
    [JsonPropertyName("township")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string Township { get; set; } = default!;
    /// <summary>
    /// 乡镇街道编码
    /// </summary>
    [JsonPropertyName("town_code")]
    [JsonConverter(typeof(AmapStringConverter))]
    public string TownCode { get; set; } = default!;
    /// <summary>
    /// 社区信息
    /// </summary>
    [JsonPropertyName("neighborhood")]
    public NeighborhoodOutput Neighborhood { get; set; } = default!;
    /// <summary>
    /// 楼信息
    /// </summary>
    [JsonPropertyName("building")]
    public BuildingOutput Building { get; set; } = default!;
    /// <summary>
    /// 街道信息
    /// </summary>
    [JsonPropertyName("streetNumber")]
    public StreetNumberOutput StreetNumber { get; set; } = default!;
    /// <summary>
    /// 所属海域信息
    /// </summary>
    [JsonPropertyName("seaArea")]
    public string? SeaArea { get; set; }
    /// <summary>
    /// 经纬度所属商圈列表
    /// </summary>
    [JsonPropertyName("businessAreas")]
    [JsonConverter(typeof(AmapArrayConverter<BusinessAreaOutput>))]
    public BusinessAreaOutput[]? BusinessAreas { get; set; }
}
