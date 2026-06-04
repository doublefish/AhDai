using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 结合知名地点形成的描述性地址，更具人性化特点
/// </summary>
public class FormattedAddressesOutput
{
    /// <summary>
    /// 推荐使用的地址描述，描述精确性较高
    /// </summary>
    [JsonPropertyName("recommend")]
    public string? Recommend { get; set; }
    /// <summary>
    /// 粗略位置描述
    /// </summary>
    [JsonPropertyName("rough")]
    public string? Rough { get; set; }
    /// <summary>
    /// 基于附近关键地点（POI）的精确地址
    /// </summary>
    [JsonPropertyName("standard_address")]
    public string? StandardAddress { get; set; }
}
