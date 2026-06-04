using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 支付场景信息描述
/// </summary>
public class SceneInfoOutput
{
    /// <summary>
    /// 终端设备号（门店号或收银设备ID）
    /// </summary>
    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }
}
