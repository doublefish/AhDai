using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// IpLocationOutput
/// <see href="https://lbs.qq.com/service/webService/webServiceGuide/position/webServiceIp">详细文档请参阅</see>
/// </summary>
public class IpLocationOutput
{
    /// <summary>
    /// 用于定位的IP地址
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = default!;
    /// <summary>
    /// 定位坐标。注：IP定位服务精确到市级，该位置为IP地址所属的行政区划政府坐标。
    /// </summary>
    [JsonPropertyName("location")]
    public LocationOutput Location { get; set; } = default!;
    /// <summary>
    /// 定位行政区划信息
    /// </summary>
    [JsonPropertyName("ad_info")]
    public AdInfoOutput AdInfo { get; set; } = default!;
}
