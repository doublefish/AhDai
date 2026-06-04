using AhDai.Core.Utils;
using AhDai.ExternalService.Aliyun.Configs;
using AhDai.ExternalService.Aliyun.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

internal class AliyunOcrTestService(AliyunOcrConfig config)
{
    readonly ILogger _logger = LoggerUtil.GetLogger<AliyunOcrTestService>();
    readonly AliyunOcrConfig _config = config;

    protected AlibabaCloud.OpenApiClient.Client GetClient() => new(new AlibabaCloud.OpenApiClient.Models.Config()
    {
        AccessKeyId = _config.AccessKeyId,
        AccessKeySecret = _config.AccessKeySecret,
        Endpoint = _config.Host[8..],
    });

    public async Task<OcrTaxPaymentCertificateOutput> TaxPaymentCertificateAsync(string? url, Stream? stream)
    {
        var query = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(url))
        {
            query.Add("Url", url);
        }
        var paras = new AlibabaCloud.OpenApiClient.Models.Params()
        {
            Action = "RecognizeTaxClearanceCertificate",
            Version = "2021-07-07",
            Protocol = "HTTPS",
            Pathname = "/",
            Method = "POST",
            AuthType = "AK",
            BodyType = "json",
            ReqBodyType = "formData",
            Style = "RPC",
        };
        var runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions()
        {
            HttpProxy = "http://127.0.0.1:8888",
            NoProxy = "localhost,127.0.0.1",
        };
        var request = new AlibabaCloud.OpenApiClient.Models.OpenApiRequest()
        {
            //Query = AlibabaCloud.OpenApiUtil.Client.Query(query),
            //Body = ,
            Stream = stream,
        };

        var client = GetClient();
        var resp = await client.CallApiAsync(paras, request, runtime);
        if (resp.TryGetValue("body", out var body))
        {
            var temp = JsonUtil.Serialize(body);
            return JsonUtil.Deserialize<OcrTaxPaymentCertificateOutput>(temp) ?? new OcrTaxPaymentCertificateOutput();
        }
        _logger.LogInformation("请求阿里云文字识别服务结果=>{resp}", JsonUtil.Serialize(resp));
        return new OcrTaxPaymentCertificateOutput();
    }
}
