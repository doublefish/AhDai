using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备资源详情
/// </summary>
public class DeviceResourceOutput
{
    /// <summary>
    /// 设备序列号
    /// </summary>
    [JsonPropertyName("deviceSerial")]
    public string DeviceSerial { get; set; } = default!;
    /// <summary>
    /// 通道号
    /// </summary>
    [JsonPropertyName("logicNo")]
    public int LogicNo { get; set; }
    /// <summary>
    /// 是否在线 0-离线 1-在线 -1空闲
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }
    /// <summary>
    /// 是否加密
    /// </summary>
    [JsonPropertyName("isEncrypt")]
    public int IsEncrypt { get; set; }
    /// <summary>
    /// 主设备序列号
    /// </summary>
    [JsonPropertyName("gwDeviceSerial")]
    public string GwDeviceSerial { get; set; } = default!;
    /// <summary>
    /// 资源名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 设备名称
    /// </summary>
    [JsonPropertyName("gwName")]
    public string? GwName { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;
    /// <summary>
    /// 版本号
    /// </summary>
    [JsonPropertyName("deviceVersion")]
    public string DeviceVersion { get; set; } = default!;
    /// <summary>
    /// 设备图标
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public string? IconUrl { get; set; }
}