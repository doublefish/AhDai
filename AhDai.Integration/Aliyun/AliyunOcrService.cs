using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models.Ocr;
using AhDai.Integration.Aliyun.Providers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunOcrService
/// </summary>
[Attributes.Service()]
internal class AliyunOcrService(IAliyunOcrConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseAliyunService<AliyunOcrConfig, IAliyunOcrConfigProvider>(configProvider, httpClientFactory, "2021-07-07", 1)
    , IAliyunOcrService
{
    protected override string ServiceName => "阿里云文字识别";


    public async Task<TaxPaymentCertificateOutput> TaxPaymentCertificateAsync(string? url, Stream? stream)
    {
        //var query = Utils.ObjectUtl.ToSortedDictionary(input);
        var query = new SortedDictionary<string, string?>()
        {
            { "Url", url ?? "" }
        };
        StreamContent? content = null;
        if (stream != null)
        {
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            content = new StreamContent(ms);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        }
        //var res = await new AliyunOcrTestService(Config).TaxPaymentCertificateAsync(url, stream);
        return await SendAsync<TaxPaymentCertificateOutput>(HttpMethod.Post, "RecognizeTaxClearanceCertificate", content, query);
    }
}
