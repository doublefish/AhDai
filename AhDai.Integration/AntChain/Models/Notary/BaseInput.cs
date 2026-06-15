using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// BaseInput
/// </summary>
public abstract class BaseInput
{
    /// <summary>
    /// 产品实例Id
    /// </summary>
    [JsonPropertyName("product_instance_id")]
    public virtual string? ProductInstanceId { get; set; }

}
