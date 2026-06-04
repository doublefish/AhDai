using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// OceanOutput
/// </summary>
public class OceanOutput
{
    /// <summary>
    /// 地点唯一标识
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 名称/标题
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}
