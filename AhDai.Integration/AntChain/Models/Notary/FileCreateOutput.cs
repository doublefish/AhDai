using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// FileCreateOutput
/// </summary>
public class FileCreateOutput : BaseOutput
{
    /// <summary>
    /// 存证凭据，全局唯一
    /// </summary>
    [JsonPropertyName("tx_hash")]
    public string TxHash { get; set; } = default!;
}
