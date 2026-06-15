using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// FileLocationInput
/// </summary>
public class FileLocationInput
{
    /// <summary>
    /// 所在城市
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }
    /// <summary>
    /// 使用设备的IMEI号
    /// </summary>
    [JsonPropertyName("imei")]
    public string? IMEI { get; set; }
    /// <summary>
    /// 使用设备的IMSI号
    /// </summary>
    [JsonPropertyName("imsi")]
    public string? IMSI { get; set; }
    /// <summary>
    /// 使用设备的IP地址
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
    /// <summary>
    /// 纬度
    /// </summary>
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }
    /// <summary>
    /// 经度
    /// </summary>
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }
    /// <summary>
    /// 使用设备的Wi-Fi物理地址
    /// </summary>
    [JsonPropertyName("mac_addr")]
    public string? MacAddr { get; set; }
}
