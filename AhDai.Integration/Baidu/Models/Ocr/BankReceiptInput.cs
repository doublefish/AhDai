using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// BankReceiptInput
/// </summary>
public class BankReceiptInput : BaseDocumentInput
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
