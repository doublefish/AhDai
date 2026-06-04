using AhDai.Core.Utils;
using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

internal class AliyunVodTestService(AliyunVodConfig config)
{
    readonly ILogger _logger = LoggerUtil.GetLogger<AliyunVodTestService>();
    readonly AliyunVodConfig _config = config;

    protected AlibabaCloud.OpenApiClient.Client GetClient() => new(new AlibabaCloud.OpenApiClient.Models.Config()
    {
        AccessKeyId = _config.AccessKeyId,
        AccessKeySecret = _config.AccessKeySecret,
        Endpoint = _config.Host[8..],
    });

    public async Task<GetPlayInfoOutput> GetPlayInfoAsync(IDictionary<string, string> query)
    {
        var paras = new AlibabaCloud.OpenApiClient.Models.Params()
        {
            Action = "GetPlayInfo",
            Version = "2017-03-21",
            Protocol = "HTTPS",
            Pathname = "/",
            Method = "POST",
            AuthType = "AK",
            BodyType = "json",
            ReqBodyType = "json",
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
        };
        var client = GetClient();
        var resp = await client.CallApiAsync(paras, request, runtime);
        _logger.LogInformation("请求阿里云点播服务结果=>{resp}", JsonUtil.Serialize(resp));
        if (resp.TryGetValue("body", out var body))
        {
            var temp = JsonUtil.Serialize(body);
            return JsonUtil.Deserialize<GetPlayInfoOutput>(temp) ?? new GetPlayInfoOutput();
        }
        return new GetPlayInfoOutput();
    }
}
