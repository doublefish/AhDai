namespace AhDai.Integration.Models.Ocr;

/// <summary>
/// 完税证明文字识别
/// </summary>
public class TaxPaymentCertificateFriendlyOutput : BaseFriendlyOutput
{
    /// <summary>
    /// 数据
    /// </summary>
    public Aliyun.Models.Ocr.TaxClearanceOutput? Data { get; set; }
}
