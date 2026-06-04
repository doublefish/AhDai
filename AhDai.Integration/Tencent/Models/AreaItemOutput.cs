using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// AreaItemOutput
/// </summary>
public class AreaItemOutput
{
    /// <summary>
    /// 地点唯一标识
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// 名称/标题
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// 坐标
    /// </summary>
    [JsonPropertyName("location")]
    public LocationOutput? Location { get; set; }
    /// <summary>
    /// 此参考位置到输入坐标的直线距离
    /// </summary>
    [JsonPropertyName("_distance")]
    public double? Distance { get; set; }
    /// <summary>
    /// 此参考位置到输入坐标的方位关系，如：北、南、内
    /// </summary>
    [JsonPropertyName("_dir_desc")]
    public string? DirDesc { get; set; }
}
