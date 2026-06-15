using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// AccurateBasicInput
/// </summary>
public class AccurateBasicInput : BaseDocumentInput
{
    /// <summary>
    /// 识别语言类型，默认为CHN_ENG
    /// 可选值包括：auto_detect/CHN_ENG/...
    /// </summary>
    [JsonPropertyName("language_type")]
    public string? LanguageType { get; set; }
    /// <summary>是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
    /// - true：检测朝向；
    /// - false：不检测朝向
    /// </summary>
    [JsonPropertyName("detect_direction")]
    public string? DetectDirection { get; set; }
    /// <summary>
    /// 是否输出段落信息
    /// true/false
    /// </summary>
    [JsonPropertyName("paragraph")]
    public string? Paragraph { get; set; }
    /// <summary>
    /// 是否返回识别结果中每一行的置信度
    /// true/false
    /// </summary>
    [JsonPropertyName("probability")]
    public string? Probability { get; set; }
    /// <summary>
    /// 是否开启行级别的多方向文字识别，可选值包括:
    /// - true：识别
    /// - false：不识别
    /// 若图内有不同方向的文字时，建议将此参数设置为“true”
    /// </summary>
    [JsonPropertyName("multidirectional_recognize")]
    public string? MultidirectionalRecognize { get; set; }
}
