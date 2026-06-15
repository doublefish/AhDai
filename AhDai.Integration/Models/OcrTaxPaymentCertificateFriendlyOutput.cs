namespace AhDai.Integration.Models;

/// <summary>
/// 完税证明文字识别
/// </summary>
public class OcrTaxPaymentCertificateFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 数据
    /// </summary>
    public Aliyun.Models.Ocr.TaxClearanceOutput? Data { get; set; }
}
