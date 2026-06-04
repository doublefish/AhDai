using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// GeocoderOutput
/// <see href="https://lbs.qq.com/service/webService/webServiceGuide/address/Gcoder">详细文档请参阅</see>
/// </summary>
public class GeocoderOutput
{
    /// <summary>
    /// 以行政区划+道路+门牌号等信息组成的标准格式化地址
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 结合了地点、附近道路等形成的综合描述
    /// </summary>
    [JsonPropertyName("formatted_addresses")]
    public FormattedAddressesOutput? FormattedAddresses { get; set; }
    /// <summary>
    /// 地址部件，address不满足需求时可自行拼接
    /// </summary>
    [JsonPropertyName("address_component")]
    public AddressComponentOutput AddressComponent { get; set; } = default!;
    /// <summary>
    /// 行政区划信息
    /// </summary>
    [JsonPropertyName("ad_info")]
    public GeocoderAdInfoOutput AdInfo { get; set; } = default!;
    /// <summary>
    /// 坐标相对位置参考
    /// </summary>
    [JsonPropertyName("address_reference")]
    public AddressReferenceOutput? AddressReference { get; set; }
    /// <summary>
    /// 查询的周边poi的总数，仅在传入参数get_poi=1时返回
    /// </summary>
    [JsonPropertyName("poi_count")]
    public int? PoiCount { get; set; }
    /// <summary>
    /// 周边地点（POI/AOI）列表，数组中每个子项为一个POI/AOI对象
    /// 说明：POI即地点，如一个便利店，往往因其面积较小，其位置一般仅会标为为一个点，而学校、小区等往往面积较大，通常会有一定的地理范围，即所谓AOI，
    /// 如果所请求的经纬度在AOI内，其距离会为0，且方位描述为“内”，如果是一个面积较小的地点，或不在AOI内，距离会>0，方位描述会为具体方位词，如“东”
    /// </summary>
    [JsonPropertyName("pois")]
    public PoiOutput[]? Pois { get; set; }
}
