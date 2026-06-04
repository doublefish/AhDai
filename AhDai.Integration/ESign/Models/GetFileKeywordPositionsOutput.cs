using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 检索文件关键字坐标
/// </summary>
public class GetFileKeywordPositionsOutput
{
    /// <summary>
    /// 关键字位置
    /// </summary>
    [JsonPropertyName("keywordPositions")]
    public GetFileKeywordPositionOutput[] KeywordPositions { get; set; } = default!;
}
