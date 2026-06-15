using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TransGetOutput
/// </summary>
public class TransGetOutput : BaseOutput
{
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
    /// <summary>
    /// 存证事务相关信息的可下载url
    /// </summary>
    [JsonPropertyName("file_url")]
    public string[] FileUrl { get; set; } = default!;
}
