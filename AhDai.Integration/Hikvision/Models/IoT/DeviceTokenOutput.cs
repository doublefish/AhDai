using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备token
/// </summary>
public class DeviceTokenOutput
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
    /// token过期时间到，时间戳，精确到秒
    /// </summary>
    [JsonPropertyName("tokenExpire")]
    public int TokenExpire { get; set; }
    /// <summary>
    /// streamToken取流播放过期时间到，时间戳，精确到秒
    /// </summary>
    [JsonPropertyName("streamTokenPlayExpire")]
    public int StreamTokenPlayExpire { get; set; }
    /// <summary>
    /// streamToken预览
    /// </summary>
    [JsonPropertyName("streamLiveToken")]
    public string StreamLiveToken { get; set; } = default!;
    /// <summary>
    /// streamToken回放
    /// </summary>
    [JsonPropertyName("streamRecToken")]
    public string StreamRecToken { get; set; } = default!;
    /// <summary>
    /// streamToken对讲
    /// </summary>
    [JsonPropertyName("streamTalkToken")]
    public string StreamTalkToken { get; set; } = default!;
    /// <summary>
    /// 设备类型token。resourceCategory=Global channelNo=*
    /// </summary>
    [JsonPropertyName("deviceToken")]
    public string DeviceToken { get; set; } = default!;
    /// <summary>
    /// 设备Global类型token。resourceCategory=Global channelNo=具体通道号
    /// </summary>
    [JsonPropertyName("deviceGlobalToken")]
    public string DeviceGlobalToken { get; set; } = default!;
    /// <summary>
    /// 设备Video类型token。 resourceCategory=Video channelNo=具体通道号
    /// </summary>
    [JsonPropertyName("deviceVideoToken")]
    public string DeviceVideoToken { get; set; } = default!;
    /// <summary>
    /// 非设备操作的token
    /// </summary>
    [JsonPropertyName("httpUrlToken")]
    public string HttpUrlToken { get; set; } = default!;
}
