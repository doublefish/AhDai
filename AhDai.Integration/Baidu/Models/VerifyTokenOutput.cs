using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// VerifyTokenOutput
/// </summary>
public class VerifyTokenOutput
{
    /// <summary>
    /// VerifyToken
    /// </summary>
    [JsonPropertyName("verify_token")]
    public string VerifyToken { get; set; } = default!;
}
