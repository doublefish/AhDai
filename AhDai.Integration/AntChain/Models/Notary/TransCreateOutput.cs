using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TransCreateOutput
/// </summary>
public class TransCreateOutput : BaseOutput
{
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
}
