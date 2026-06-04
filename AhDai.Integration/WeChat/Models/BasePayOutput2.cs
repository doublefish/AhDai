using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// BasePayOutput2
/// </summary>
public class BasePayOutput2
{
    /// <summary>
    /// 返回状态码
    /// </summary>
    [JsonPropertyName("return_code")]
    public string? ReturnCode { get; set; }
    /// <summary>
    /// 错误码描述	
    /// </summary>
    [JsonPropertyName("return_msg")]
    public string? ReturnMessage { get; set; }
    /// <summary>
    /// 错误码
    /// </summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }
}
