using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrBankReceiptWordsResult
/// </summary>
public class OcrBankReceiptWordsResult
{
    /// <summary>
    /// 字段识别结果
    /// </summary>
    [JsonPropertyName("word")]
    public string Word { get; set; } = default!;
    /// <summary>
    /// 字段识别结果置信度，当请求参数 probability=true 时返回该字段
    /// </summary>
    [JsonPropertyName("probability")]
    public OcrWordProbabilityOutput? Probability { get; set; }
    /// <summary>
    /// 字段位置信息，当请求参数 location=true 时返回该字段
    /// </summary>
    [JsonPropertyName("location")]
    public OcrWordLocationOutput? Location { get; set; }

}
