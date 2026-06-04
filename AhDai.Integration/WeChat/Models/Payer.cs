using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 支付者信息
/// </summary>
public class Payer
{
    /// <summary>
    /// 用户在直连商户AppID下的唯一标识
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; } = default!;
}
