using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 坐标
/// </summary>
public class CoordinateOutput
{
    /// <summary>
    /// X坐标
    /// </summary>
    [JsonPropertyName("positionX")]
    public float PositionX { get; set; }
    /// <summary>
    /// Y坐标
    /// </summary>
    [JsonPropertyName("positionY")]
    public float PositionY { get; set; }
}
