using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryCertificateGetInput
/// </summary>
public class TwcNotaryCertificateDetailGetInput : BaseTwcNotaryInput
{
    /// <summary>
    /// 存证凭据，全局唯一
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("tx_hash")]
    public string TxHash { get; set; } = default!;
}
