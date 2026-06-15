using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// 识别身份证质量类型
/// </summary>
public class IdCardQualityOutput
{
    /// <summary>
    /// 质量类型，是否清晰，返回值包括：
    /// - 1：清晰
    /// - 0：不清晰
    /// </summary>
    [JsonPropertyName("IsClear")]
    public float IsClear { get; set; }
    /// <summary>
    /// “是否清晰”质量类型对应的概率，值在0-1之间，值越大表示图像质量越好。默认阈值（仅为推荐值，建议按照实际业务场景，基于图片返回的具体概率值，自定义设置判断阈值）：当 IsClear_propobility 超过0.5时，对应 IsClear 返回1，低于0.5，则返回0
    /// </summary>
    [JsonPropertyName("IsClear_propobility")]
    public float IsClearPropobility { get; set; }
    /// <summary>
    /// 质量类型，是否边框/四角完整，返回值包括：
    /// - 1：边框/四角完整
    /// - 0：边框/四角不完整
    /// </summary>
    [JsonPropertyName("IsComplete")]
    public float IsComplete { get; set; }
    /// <summary>
    /// “是否边框/四角完整”质量类型对应的概率，值在0-1之间，值越大表示图像质量越好。默认阈值（仅为推荐值，建议按照实际业务场景，基于图片返回的具体概率值，自定义设置判断阈值）：当 IsComplete_propobility 超过0.5时，对应 IsComplete 返回1，低于0.5，则返回0
    /// </summary>
    [JsonPropertyName("IsComplete_propobility")]
    public float IsCompletePropobility { get; set; }
    /// <summary>
    /// 质量类型，是否头像、关键字段无遮挡/马赛克，返回值包括：
    /// - 1：头像、关键字段无遮挡/马赛克
    /// - 0：头像、关键字段有遮挡/马赛克
    /// </summary>
    [JsonPropertyName("IsNoCover")]
    public float IsNoCover { get; set; }
    /// <summary>
    /// “是否头像、关键字段无遮挡/马赛克”质量类型对应的概率，值在0-1之间，值越大表示图像质量越好。默认阈值（仅为推荐值，建议按照实际业务场景，基于图片返回的具体概率值，自定义设置判断阈值）：当 IsNoCover_propobility 超过0.3时，对应IsNoCover 返回1，低于0.3，则返回0
    /// </summary>
    [JsonPropertyName("IsNoCover_propobility")]
    public float IsNoCoverPropobility { get; set; }
}
