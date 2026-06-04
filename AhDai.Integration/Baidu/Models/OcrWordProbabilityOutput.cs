using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrWordProbabilityOutput
/// </summary>
public class OcrWordProbabilityOutput
{
    /// <summary>
    /// 字段识别结果中各字符的置信度平均值
    /// </summary>
    [JsonPropertyName("average")]
    public float Average { get; set; }
    /// <summary>
    /// 字段识别结果中各字符的置信度最小值
    /// </summary>
    [JsonPropertyName("min")]
    public float Min { get; set; }
}
