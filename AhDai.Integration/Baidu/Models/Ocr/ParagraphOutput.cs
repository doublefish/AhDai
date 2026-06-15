using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// ParagraphOutput
/// </summary>
public class ParagraphOutput
{
    /// <summary>
    /// 一个段落包含的行序号，当 paragraph=true 时返回该字段
    /// </summary>
    [JsonPropertyName("words_result_idx")]
    public int[] WordsResultIdx { get; set; } = default!;
}
