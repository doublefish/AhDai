using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// BaseSmsOutput
/// </summary>
public class BaseSmsOutput
{
    /// <summary>
    /// 请求状态码，返回 OK 代表请求成功
    /// </summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; } = default!;
    /// <summary>
    /// 状态码的描述
    /// </summary>
    [JsonPropertyName("Message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// 发送回执ID
    /// </summary>
    [JsonPropertyName("BizId")]
    public string BizId { get; set; } = default!;
    /// <summary>
    /// 请求ID
    /// </summary>
    [JsonPropertyName("RequestId")]
    public string RequestId { get; set; } = default!;
}
