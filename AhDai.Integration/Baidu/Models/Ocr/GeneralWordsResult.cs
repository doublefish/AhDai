using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// GeneralWordsResult
/// </summary>
public class GeneralWordsResult : WordsResult
{
    /// <summary>
    /// 字段位置信息，当请求参数 location=true 时返回该字段
    /// </summary>
    [JsonPropertyName("location")]
    public WordLocationOutput? Location { get; set; }
    /// <summary>
    /// 识别结果中每一行的置信度值，包含average：行置信度平均值，variance：行置信度方差，min：行置信度最小值，当 probability=true 时返回该字段
    /// </summary>
    [JsonPropertyName("probability")]
    public ProbabilityOutput? Probability { get; set; }
}
