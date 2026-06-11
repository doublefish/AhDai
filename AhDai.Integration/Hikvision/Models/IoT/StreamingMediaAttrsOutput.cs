using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备取流清晰度
/// </summary>
public class StreamingMediaAttrsOutput
{
    /// <summary>
    /// 当前视频清晰度，0流畅、1均衡、2高清、3超清、4极清、其他是未知
    /// </summary>
    [JsonPropertyName("videoLevel")]
    public int VideoLevel { get; set; }
    /// <summary>
    /// traceId
    /// </summary>
    [JsonPropertyName("traceId")]
    public string TraceId { get; set; } = default!;
}
