using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备取流清晰度
/// </summary>
public class StreamingMediaAttrsInput
{
    /// <summary>
    /// 设备序列号
    /// </summary>
    [Required]
    [JsonPropertyName("deviceSerial")]
    public string DeviceSerial { get; set; } = default!;
    /// <summary>
    /// 载荷
    /// </summary>
    [JsonPropertyName("payload")]
    public StreamingMediaAttrsPayload Payload { get; set; } = default!;
}
