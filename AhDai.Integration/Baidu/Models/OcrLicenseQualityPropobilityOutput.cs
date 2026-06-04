using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 识别质量检测置信度
/// </summary>
public class OcrLicenseQualityPropobilityOutput
{
    /// <summary>
    /// “是否清晰”质量类型对应的概率，值在0-1之间，值越大表示图像质量越好。默认阈值（仅为推荐值，建议按照实际业务场景，基于图片返回的具体概率值，自定义设置判断阈值）：当 is_clear_propobility 超过 0.5 时，对应 warn_infos 返回 fuzzy，低于 0.5，则不返回 fuzzy
    /// </summary>
    [JsonPropertyName("is_clear_propobility")]
    public float IsClearPropobility { get; set; }
    /// <summary>
    /// “是否边框/四角完整”质量类型对应的概率，值在0-1之间，值越大表示图像质量越好。默认阈值（仅为推荐值，建议按照实际业务场景，基于图片返回的具体概率值，自定义设置判断阈值）：当 is_complete_propobility 超过 0.5 时，对应 warn_infos 返回 incomplete，低于0.5，则不返回 incomplete
    /// </summary>
    [JsonPropertyName("is_complete_propobility")]
    public float IsCompletePropobility { get; set; }
    /// <summary>
    /// “是否被遮挡”质量类型对应的概率，值取0或1，0代表图像被遮挡，1代表图像没有被遮挡
    /// </summary>
    [JsonPropertyName("is_noshield_propobility")]
    public float IsNoshieldPropobility { get; set; }
}
