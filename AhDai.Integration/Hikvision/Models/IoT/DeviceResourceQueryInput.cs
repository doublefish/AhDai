using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备资源详情查询
/// </summary>
public class DeviceResourceQueryInput
{
    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }
    /// <summary>
    /// 设备序列号
    /// </summary>
    [JsonPropertyName("deviceSerial")]
    public string DeviceSerial { get; set; } = default!;
    /// <summary>
    /// 通道号
    /// </summary>
    [JsonPropertyName("channelNo")]
    public int ChannelNo { get; set; }
}
