using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 单个获取设备token
/// </summary>
public class DeviceTokenInput
{
    /// <summary>
    /// 设备序列号
    /// </summary>
    [Required]
    [JsonPropertyName("deviceSerial")]
    public string DeviceSerial { get; set; } = default!;
    /// <summary>
    /// 通道号
    /// </summary>
    [JsonPropertyName("channelNo")]
    public int? ChannelNo { get; set; }
}
