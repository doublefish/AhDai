using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 行政区划信息
/// </summary>
public class AdInfoOutput
{
    /// <summary>
    /// 国家
    /// </summary>
    [JsonPropertyName("nation")]
    public string Nation { get; set; } = default!;
    /// <summary>
    /// 国家代码（ISO3166标准3位数字码）
    /// </summary>
    [JsonPropertyName("nation_code")]
    public int NationCode { get; set; }
    /// <summary>
    /// 省
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 市
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }
    /// <summary>
    /// 区
    /// </summary>
    [JsonPropertyName("district")]
    public string? District { get; set; }
    /// <summary>
    /// 行政区划代码
    /// </summary>
    [JsonPropertyName("adcode")]
    public int AdCode { get; set; }
}
