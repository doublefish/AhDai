using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models.Oss;

/// <summary>
/// PolicyConfig
/// </summary>
internal class PolicyConfig
{
    /// <summary>
    /// Expiration
    /// </summary>
    [JsonPropertyName("expiration")]
    public string Expiration { get; set; } = default!;
    /// <summary>
    /// Conditions
    /// </summary>
    [JsonPropertyName("conditions")]
    public List<List<object>>? Conditions { get; set; }
}
