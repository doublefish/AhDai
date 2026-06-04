using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// BaseOutput
/// </summary>
public class BaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("errcode")]
    public int? ErrorCode { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("errmsg")]
    public string? ErrorMessage { get; set; }
}
