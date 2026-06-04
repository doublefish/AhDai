using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrAccurateBasicOutput
/// </summary>
public class OcrAccurateBasicOutput : BaseDocumentOcrOutput<OcrGeneralWordsResult[]>
{
    /// <summary>
    /// 图像方向，当 detect_direction=true 时返回该字段。
    /// - -1：未定义，
    /// - 0：正向，
    /// - 1：逆时针90度，
    /// - 2：逆时针180度，
    /// - 3：逆时针270度
    /// </summary>
    [JsonPropertyName("direction")]
    public int? Direction { get; set; }
    /// <summary>
    /// 段落检测结果，当 paragraph=true 时返回该字段
    /// </summary>
    [JsonPropertyName("paragraphs_result")]
    public OcrParagraphOutput[]? ParagraphsResult { get; set; }
    /// <summary>
    /// 识别结果数，表示 paragraphs_result的元素个数，当 paragraph=true 时返回该字段
    /// </summary>
    [JsonPropertyName("paragraphs_result_num")]
    public uint? ParagraphsResultNum { get; set; }

    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrAccurateBasicFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrAccurateBasicFriendlyOutput();
        var list = new List<string>();
        foreach (var words in WordsResult)
        {
            list.Add(words.Words);
        }
        output.Words = list.ToArray();
        return output;
    }
}
