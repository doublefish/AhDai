using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 分页查询
/// </summary>
public class PageInput
{
    /// <summary>
    /// 第几页，默认值1
    /// </summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }
    /// <summary>
    /// 每页显示数量，默认值20	
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
