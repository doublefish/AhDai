using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 实时方案视频
/// </summary>
public class MediaResultOutput
{
    /// <summary>
    /// 实时核验视频链接，第一个为人脸核验视频，第二个是意愿核验视频
    /// </summary>
    [JsonPropertyName("processVideo")]
    public string[] ProcessVideo { get; set; } = default!;
    /// <summary>
    /// 图片链接数组，H5实时炫瞳活体、H5实时动作活体、H5实时静默活体方案所返回的4张人脸图片
    /// </summary>
    [JsonPropertyName("images")]
    public string[] Images { get; set; } = default!;
    /// <summary>
    /// 意愿核验方案不使用该字段
    /// </summary>
    [JsonPropertyName("video")]
    public object[]? Video { get; set; }
    /// <summary>
    /// 扩展信息，如有则代表处理视频中的错误信息
    /// </summary>
    [JsonPropertyName("extInfo")]
    public object? ExtInfo { get; set; }
}
