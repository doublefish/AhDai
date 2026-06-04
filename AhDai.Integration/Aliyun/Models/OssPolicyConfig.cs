using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// OssPolicyConfig
/// </summary>
internal class OssPolicyConfig
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
