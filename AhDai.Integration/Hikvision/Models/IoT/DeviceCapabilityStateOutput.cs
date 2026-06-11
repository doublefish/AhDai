using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 能力状态
/// </summary>
public class DeviceCapabilityStateOutput
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("stateDesc")]
    public string StateDesc { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("entrance")]
    public string Entrance { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("canBuy")]
    public bool CanBuy { get; set; }
}
