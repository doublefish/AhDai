using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备取流清晰度载荷
/// </summary>
public class StreamingMediaAttrsPayload
{
    /// <summary>
    /// 通道号
    /// </summary>
    [Required]
    [JsonPropertyName("channelNo")]
    public int ChannelNo { get; set; }
    /// <summary>
    /// videoLevel视频清晰度
    /// </summary>
    [JsonPropertyName("attrs")]
    public string[] Attrs { get; set; } = default!;
}
