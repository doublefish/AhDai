using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// native下单
/// </summary>
public class NativeOrderOutput : BaseOutput
{
    /// <summary>
    /// 二维码链接
    /// </summary>
    [JsonPropertyName("code_url")]
    public string CodeUrl { get; set; } = default!;
}
