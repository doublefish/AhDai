using AhDai.Integration.Baidu.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaiduOcrService
/// </summary>
public interface IBaiduOcrService : IBaiduService
{
    // 通用文字识别（标准版）
    //Task<OcrGeneralBasicOutput> GeneralBasicAsync(OcrGeneralBasicInput input, string accessToken);

    /// <summary>
    /// 通用文字识别（高精度版）
    /// </summary>
    Task<OcrAccurateBasicOutput> AccurateBasicAsync(OcrAccurateBasicInput input, string accessToken);

    /// <summary>
    /// 身份证识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrIdCardOutput> IdCardAsync(OcrIdCardInput input, string accessToken);

    /// <summary>
    /// 驾驶证识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrDrivingLicenseOutput> DrivingLicenseAsync(OcrDrivingLicenseInput input, string accessToken);

    /// <summary>
    /// 行驶证识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrVehicleLicenseOutput> VehicleLicenseAsync(OcrVehicleLicenseInput input, string accessToken);

    /// <summary>
    /// 道路运输证识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrRoadTransportCertificateOutput> RoadTransportCertificateAsync(OcrRoadTransportCertificateInput input, string accessToken);

    /// <summary>
    /// 增值税发票识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrVatInvoiceOutput> VatInvoiceAsync(OcrVatInvoiceInput input, string accessToken);

    /// <summary>
    /// 银行回单识别
    /// </summary>
    /// <param name="input"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<OcrBankReceiptOutput> BankReceiptAsync(OcrBankReceiptInput input, string accessToken);
}
