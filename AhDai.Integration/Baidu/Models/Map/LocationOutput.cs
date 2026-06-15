using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// LocationOutput
/// </summary>
public class LocationOutput
{
    /// <summary>
    /// 纬度
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    /// <summary>
    /// 经度
    /// </summary>
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}
