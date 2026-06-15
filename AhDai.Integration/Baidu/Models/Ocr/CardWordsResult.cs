using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// CardWordsResult
/// </summary>
public class CardWordsResult : WordsResult
{
    /// <summary>
    /// 字段位置信息，当请求参数 location=true 时返回该字段
    /// </summary>
    [JsonPropertyName("location")]
    public WordLocationOutput? Location { get; set; }
}
