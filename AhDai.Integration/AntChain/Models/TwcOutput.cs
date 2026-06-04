using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcOutput
/// </summary>
/// <typeparam name="T"></typeparam>
public class TwcOutput<T>
{
    /// <summary>
    /// 响应
    /// </summary>
    [JsonPropertyName("response")]
    public T? Response { get; set; }
    /// <summary>
    /// 签名
    /// </summary>
    [JsonPropertyName("sign")]
    public string? Sign { get; set; }

}
