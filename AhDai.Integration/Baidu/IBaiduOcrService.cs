using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models.Ocr;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaiduOcrService
/// </summary>
public interface IBaiduOcrService : IBaseBaiduService<BaiduOcrConfig>
{
    // 通用文字识别（标准版）
    //Task<OcrGeneralBasicOutput> GeneralBasicAsync(OcrGeneralBasicInput input, string accessToken);

    /// <summary>
    /// 通用文字识别（高精度版）
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AccurateBasicOutput> AccurateBasicAsync(string accessToken, AccurateBasicInput input);

    /// <summary>
    /// 身份证识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IdCardOutput> IdCardAsync(string accessToken, IdCardInput input);

    /// <summary>
    /// 驾驶证识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DrivingLicenseOutput> DrivingLicenseAsync(string accessToken, DrivingLicenseInput input);

    /// <summary>
    /// 行驶证识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<VehicleLicenseOutput> VehicleLicenseAsync(string accessToken, VehicleLicenseInput input);

    /// <summary>
    /// 道路运输证识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RoadTransportCertificateOutput> RoadTransportCertificateAsync(string accessToken, RoadTransportCertificateInput input);

    /// <summary>
    /// 增值税发票识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<VatInvoiceOutput> VatInvoiceAsync(string accessToken, VatInvoiceInput input);

    /// <summary>
    /// 银行回单识别
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<BankReceiptOutput> BankReceiptAsync(string accessToken, BankReceiptInput input);
}
