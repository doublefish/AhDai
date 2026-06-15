using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// VatInvoiceInput
/// </summary>
public class VatInvoiceInput : BaseDocumentInput
{
    /// <summary>
    /// 进行识别的增值税发票类型，默认为 normal，可缺省
    /// - **normal：**可识别增值税普票、专票、电子发票
    /// - **roll：**可识别增值税卷票
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    /// <summary>
    /// 是否开启印章判断功能，并返回印章内容的识别结果
    /// - **true：**开启
    /// - **false：**不开启
    /// </summary>
    [JsonPropertyName("seal_tag")]
    public string? SealTag { get; set; }
}
