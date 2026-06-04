using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 地址部件
/// </summary>
public class AddressComponentOutput
{
    /// <summary>
    /// 国家
    /// </summary>
    [JsonPropertyName("nation")]
    public string Nation { get; set; } = default!;
    /// <summary>
    /// 省
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 市，如果当前城市为省直辖县级区划，city与district字段均会返回此城市
    /// 注：省直辖县级区划adcode第3和第4位分别为9、0，如济源市adcode为419001
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = default!;
    /// <summary>
    /// 区，可能为空字串
    /// </summary>
    [JsonPropertyName("district")]
    public string? District { get; set; }
    /// <summary>
    /// 道路，可能为空字串
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    /// <summary>
    /// 门牌，可能为空字串
    /// </summary>
    [JsonPropertyName("street_number")]
    public string? StreetNumber { get; set; }
}
