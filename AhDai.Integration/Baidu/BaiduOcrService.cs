using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduOcrService
/// </summary>
internal class BaiduOcrService(IBaseRedisService redisService, IBaiduConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaiduService(redisService, configProvider, httpClientFactory), IBaiduOcrService
{
    public async Task<OcrAccurateBasicOutput> AccurateBasicAsync(OcrAccurateBasicInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/accurate_basic?access_token={accessToken}";
        return await SendAsync<OcrAccurateBasicOutput, OcrAccurateBasicInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<OcrIdCardOutput> IdCardAsync(OcrIdCardInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/idcard?access_token={accessToken}";
        return await SendAsync<OcrIdCardOutput, OcrIdCardInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<OcrDrivingLicenseOutput> DrivingLicenseAsync(OcrDrivingLicenseInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/driving_license?access_token={accessToken}";
        return await SendAsync<OcrDrivingLicenseOutput, OcrDrivingLicenseInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<OcrVehicleLicenseOutput> VehicleLicenseAsync(OcrVehicleLicenseInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/vehicle_license?access_token={accessToken}";
        return await SendAsync<OcrVehicleLicenseOutput, OcrVehicleLicenseInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<OcrRoadTransportCertificateOutput> RoadTransportCertificateAsync(OcrRoadTransportCertificateInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/road_transport_certificate?access_token={accessToken}";
        return await SendAsync<OcrRoadTransportCertificateOutput, OcrRoadTransportCertificateInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<OcrVatInvoiceOutput> VatInvoiceAsync(OcrVatInvoiceInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/vat_invoice?access_token={accessToken}";
        return await SendAsync<OcrVatInvoiceOutput, OcrVatInvoiceInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<OcrBankReceiptOutput> BankReceiptAsync(OcrBankReceiptInput input, string accessToken)
    {
        var url = $"rest/2.0/ocr/v1/bank_receipt_new?access_token={accessToken}";
        return await SendAsync<OcrBankReceiptOutput, OcrBankReceiptInput>(HttpMethod.Post, url, input, 4096);
    }


    async Task<TOutput> SendAsync<TOutput, TInput>(HttpMethod method, string url, TInput input, long maxSizeKb = 4096)
        where TOutput : IBaseOutput
        where TInput : BaseOcrInput
    {
        if (input.File != null)
        {
            var maxSize = maxSizeKb * 1024;
            if (input.File.Length > maxSize) throw new ArgumentException($"超出文件大小限制：{AhDai.Core.Utils.FileUtil.GetFileSize(maxSize)}");
            var extension = Path.GetExtension(input.File.FileName);
            var fileType = extension switch
            {
                ".jpg" or "jpeg" or ".png" or ".bmp" => 1,
                ".pdf" => 2,
                ".ofd" => 3,
                _ => throw new ArgumentException($"不支持的文件格式：{extension}"),
            };
            var base64String = await Utils.StringUtils.ConvertToBase64Async(input.File);
            input.SetFile(base64String, fileType);
        }

        var queryString = Utils.StringUtils.ObjectToQueryString(input, true, true);
        var content = new StringContent(queryString);

        var config = await GetConfigAsync();
        return await SendAsync<TOutput>(config, method, url, content);
    }

}
