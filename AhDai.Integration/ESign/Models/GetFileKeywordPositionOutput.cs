using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 检索文件关键字坐标
/// </summary>
public class GetFileKeywordPositionOutput
{
    /// <summary>
    /// 关键字
    /// </summary>
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = default!;
    /// <summary>
    /// 关键字是否检索到坐标值
    /// </summary>
    [JsonPropertyName("searchResult")]
    public bool SearchResult { get; set; }
    /// <summary>
    /// 关键字是否检索到坐标值
    /// </summary>
    [JsonPropertyName("positions")]
    public FilePositionsOutput[] Positions { get; set; } = default!;
}
