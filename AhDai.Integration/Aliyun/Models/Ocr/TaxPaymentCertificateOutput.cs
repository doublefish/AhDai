using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Models;

namespace AhDai.Integration.Aliyun.Models.Ocr;

/// <summary>
/// TaxPaymentCertificateOutput
/// </summary>
public class TaxPaymentCertificateOutput : IBaseOutput
{
    /// <summary>
    /// 请求ID
    /// </summary>
    public string RequestId { get; set; } = default!;
    /// <summary>
    /// 数据
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
    }

    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrTaxPaymentCertificateFriendlyOutput? GetFriendlyOutput()
    {
        TaxPaymentCertificateDataOutput? data = null;
        if (!string.IsNullOrEmpty(Data))
        {
            data = JsonUtil.Deserialize<TaxPaymentCertificateDataOutput>(Data);
        }
        return new OcrTaxPaymentCertificateFriendlyOutput()
        {
            Data = data?.Data,
        };
    }
}
