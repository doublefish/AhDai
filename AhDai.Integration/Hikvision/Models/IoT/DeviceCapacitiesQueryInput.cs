using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备/通道能力集
/// </summary>
public class DeviceCapacitiesQueryInput
{
    /// <summary>
    /// 设备序列号
    /// </summary>
    [Required]
    [JsonPropertyName("deviceSerial")]
    public string DeviceSerial { get; set; } = default!;
    /// <summary>
    /// 类型，默认Device
    /// Device[设备]、Channel[通道]
    /// </summary>
    [JsonPropertyName("resType")]
    public string? ResType { get; set; }
    /// <summary>
    /// 标识	
    /// 当resType=Channel时必填，代表通道号
    /// </summary>
    [JsonPropertyName("resIdentifier")]
    public string? ResIdentifier { get; set; }
}
