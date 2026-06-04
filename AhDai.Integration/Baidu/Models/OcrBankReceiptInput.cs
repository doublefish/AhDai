using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrBankReceiptInput
/// </summary>
public class OcrBankReceiptInput : BaseDocumentOcrInput
{
    /// <summary>
    /// 是否返回字段置信度，默认为 false ，即不返回
    /// </summary>
    [JsonPropertyName("probability")]
    public string? Probability { get; set; }
    /// <summary>
    /// 是否返回字段位置坐标，默认为 false，即不返回
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}
