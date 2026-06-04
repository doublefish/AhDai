using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// BasePayOutput
/// </summary>
public class BasePayOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
