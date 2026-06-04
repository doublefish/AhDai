using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// PointOutput
/// </summary>
public class PointOutput
{
    /// <summary>
    /// 经度
    /// </summary>
    [JsonPropertyName("x")]
    public string X { get; set; } = default!;
    /// <summary>
    /// 纬度
    /// </summary>
    [JsonPropertyName("y")]
    public string Y { get; set; } = default!;
}
