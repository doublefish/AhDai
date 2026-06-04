using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 地点行政区划信息
/// </summary>
public class PoiAdInfoOutput
{
    /// <summary>
    /// 行政区划代码
    /// </summary>
    [JsonPropertyName("adcode")]
    public string AdCode { get; set; } = default!;
    /// <summary>
    /// 省
    /// </summary>
    [JsonPropertyName("province")]
    public string? Province { get; set; }
    /// <summary>
    /// 市 / 地级区 及同级行政区划，如果当前城市为省直辖县级区划，city与district字段均会返回此城市
    /// 注：省直辖县级区划adcode第3和第4位分别为9、0，如济源市adcode为419001
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }
    /// <summary>
    /// 区 / 县级市 及同级行政区划
    /// </summary>
    [JsonPropertyName("district")]
    public string? District { get; set; }
}
