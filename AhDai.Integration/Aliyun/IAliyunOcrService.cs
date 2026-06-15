using AhDai.Integration.Abstractions;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models.Ocr;
using System.IO;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// IAliyunOcrService
/// </summary>
public interface IAliyunOcrService : IBaseService<AliyunOcrConfig>
{
    /// <summary>
    /// 识别完税证明
    /// </summary>
    /// <param name="url"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    Task<TaxPaymentCertificateOutput> TaxPaymentCertificateAsync(string? url, Stream? stream);
}
