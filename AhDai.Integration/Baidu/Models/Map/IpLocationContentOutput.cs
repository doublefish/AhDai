using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// IpLocationContentOutput
/// </summary>
public class IpLocationContentOutput
{
    /// <summary>
    /// 简要地址信息
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 地址明细
    /// </summary>
    [JsonPropertyName("address_detail")]
    public AddressDetailOutput AddressDetail { get; set; } = default!;
    /// <summary>
    /// 当前城市中心点坐标
    /// </summary>
    [JsonPropertyName("point")]
    public PointOutput Point { get; set; } = default!;
}
