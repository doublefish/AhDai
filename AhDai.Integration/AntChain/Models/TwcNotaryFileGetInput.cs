using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryFileGetInput
/// </summary>
public class TwcNotaryFileGetInput : BaseTwcNotaryInput
{
    /// <summary>
    /// 存证凭据，全局唯一
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("tx_hash")]
    public string TxHash { get; set; } = default!;
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

}
