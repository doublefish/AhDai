using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// BaseTwcNotaryInput
/// </summary>
public class BaseTwcNotaryInput
{
    /// <summary>
    /// 产品实例Id
    /// </summary>
    [JsonPropertyName("product_instance_id")]
    public string? ProductInstanceId { get; set; }

}
