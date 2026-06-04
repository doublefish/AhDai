using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrCardWordsResult
/// </summary>
public class OcrCardWordsResult : OcrWordsResult
{
    /// <summary>
    /// 字段位置信息，当请求参数 location=true 时返回该字段
    /// </summary>
    [JsonPropertyName("location")]
    public OcrWordLocationOutput? Location { get; set; }
}
