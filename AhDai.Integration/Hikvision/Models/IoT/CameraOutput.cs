using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 摄像头
/// </summary>
public class CameraOutput
{
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
    /// <summary>
    /// 通道序列号
    /// </summary>
    [JsonPropertyName("ipcSerial")]
    public string IpcSerial { get; set; } = default!;
    /// <summary>
    /// 设备名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 设备型号
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;
    /// <summary>
    /// 设备版本
    /// </summary>
    [JsonPropertyName("deviceVersion")]
    public string DeviceVersion { get; set; } = default!;
    /// <summary>
    /// 是否在线 0-离线 1-在线 -1空闲
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }
    /// <summary>
    /// 是否加密 0-未加密 1-加密
    /// </summary>
    [JsonPropertyName("isEncrypt")]
    public int IsEncrypt { get; set; }
}
