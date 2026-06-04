using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 文件位置
/// </summary>
public class FilePositionsOutput
{
    /// <summary>
    /// 关键字
    /// </summary>
    [JsonPropertyName("pageNum")]
    public int PageNum { get; set; }
    /// <summary>
    /// 关键字XY坐标值
    /// </summary>
    [JsonPropertyName("coordinates")]
    public CoordinateOutput[] Coordinates { get; set; } = default!;
}
