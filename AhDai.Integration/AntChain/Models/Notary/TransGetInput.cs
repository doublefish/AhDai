using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TransGetInput
/// </summary>
public class TransGetInput : BaseInput
{
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
}
