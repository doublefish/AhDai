using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryTransGetInput
/// </summary>
public class TwcNotaryTransGetInput : BaseTwcNotaryInput
{
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
}
