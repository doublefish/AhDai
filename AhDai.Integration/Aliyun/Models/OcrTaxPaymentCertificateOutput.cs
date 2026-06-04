using AhDai.Core.Utils;
using AhDai.Integration.Models;

namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// OcrTaxPaymentCertificateOutput
/// </summary>
public class OcrTaxPaymentCertificateOutput
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
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrTaxPaymentCertificateFriendlyOutput? GetFriendlyOutput()
    {
        OcrTaxPaymentCertificateDataOutput? data = null;
        if (!string.IsNullOrEmpty(Data))
        {
            data = JsonUtil.Deserialize<OcrTaxPaymentCertificateDataOutput>(Data);
        }
        return new OcrTaxPaymentCertificateFriendlyOutput()
        {
            Data = data?.Data,
        };
    }
}
