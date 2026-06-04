using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 认证人脸
/// </summary>
public class SimpleResultOutput
{
    /// <summary>
    /// 采集的用户人脸信息
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = default!;
}
