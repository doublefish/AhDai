using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// native下单
/// </summary>
public class NativeOrderOutput : BasePayOutput
{
    /// <summary>
    /// 二维码链接
    /// </summary>
    [JsonPropertyName("code_url")]
    public string CodeUrl { get; set; } = default!;
}
