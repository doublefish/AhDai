using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models.Ocr;
using AhDai.Integration.Baidu.Providers;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduOcrService
/// </summary>
[Attributes.Service()]
internal class BaiduOcrService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IBaiduOcrConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseBaiduService<BaiduOcrConfig, IBaiduOcrConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IBaiduOcrService
{
    protected override string ServiceName => "百度文字识别";


    public async Task<AccurateBasicOutput> AccurateBasicAsync(string accessToken, AccurateBasicInput input)
    {
        var url = $"rest/2.0/ocr/v1/accurate_basic?access_token={accessToken}";
        return await SendAsync<AccurateBasicOutput, AccurateBasicInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<IdCardOutput> IdCardAsync(string accessToken, IdCardInput input)
    {
        var url = $"rest/2.0/ocr/v1/idcard?access_token={accessToken}";
        return await SendAsync<IdCardOutput, IdCardInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<DrivingLicenseOutput> DrivingLicenseAsync(string accessToken, DrivingLicenseInput input)
    {
        var url = $"rest/2.0/ocr/v1/driving_license?access_token={accessToken}";
        return await SendAsync<DrivingLicenseOutput, DrivingLicenseInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<VehicleLicenseOutput> VehicleLicenseAsync(string accessToken, VehicleLicenseInput input)
    {
        var url = $"rest/2.0/ocr/v1/vehicle_license?access_token={accessToken}";
        return await SendAsync<VehicleLicenseOutput, VehicleLicenseInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<RoadTransportCertificateOutput> RoadTransportCertificateAsync(string accessToken, RoadTransportCertificateInput input)
    {
        var url = $"rest/2.0/ocr/v1/road_transport_certificate?access_token={accessToken}";
        return await SendAsync<RoadTransportCertificateOutput, RoadTransportCertificateInput>(HttpMethod.Post, url, input, 4096);
    }

    public async Task<VatInvoiceOutput> VatInvoiceAsync(string accessToken, VatInvoiceInput input)
    {
        var url = $"rest/2.0/ocr/v1/vat_invoice?access_token={accessToken}";
        return await SendAsync<VatInvoiceOutput, VatInvoiceInput>(HttpMethod.Post, url, input, 8192);
    }

    public async Task<BankReceiptOutput> BankReceiptAsync(string accessToken, BankReceiptInput input)
    {
        var url = $"rest/2.0/ocr/v1/bank_receipt_new?access_token={accessToken}";
        return await SendAsync<BankReceiptOutput, BankReceiptInput>(HttpMethod.Post, url, input, 4096);
    }


    async Task<TOutput> SendAsync<TOutput, TInput>(HttpMethod method, string url, TInput input, long maxSizeKb = 4096)
        where TOutput : IBaseOutput
        where TInput : BaseInput
    {
        if (input.File != null)
        {
            var maxSize = maxSizeKb * 1024;
            if (input.File.Length > maxSize) throw new ArgumentException($"超出文件大小限制：{Core.Utils.FileUtil.GetFileSize(maxSize)}");
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
        return await SendContentAsync<TOutput>(method, url, new StringContent(queryString, new MediaTypeHeaderValue("application/x-www-form-urlencoded")));
    }

}
