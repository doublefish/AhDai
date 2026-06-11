using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 视频清晰度选项
/// </summary>
public class DeviceVideoQualityOptionOutput
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("streamType")]
    public int StreamType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("videoLevel")]
    public int VideoLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("videoLevelDesc")]
    public string VideoLevelDesc { get; set; } = default!;
}
