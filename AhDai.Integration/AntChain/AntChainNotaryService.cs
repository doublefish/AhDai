using AhDai.Integration.AntChain.Configs;
using AhDai.Integration.AntChain.Models.Notary;
using AhDai.Integration.AntChain.Providers;
using AhDai.Integration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.AntChain;

/// <summary>
/// AntChainNotaryService
/// </summary>
[Attributes.Service()]
internal class AntChainNotaryService(IAntChainNotaryConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<AntChainNotaryConfig, IAntChainNotaryConfigProvider>(configProvider, httpClientFactory), IAntChainNotaryService
{
    protected override string ServiceName => "蚂蚁链存证";


    public async Task<TransCreateOutput> CreateTransAsync(TransCreateInput input)
    {
        if (input.Customer == null) throw new ArgumentException("关联实体的身份识别信息不可为空");
        //var res = await new AntChainNotaryTestService(Config).CreateTransAsync(input);
        return await SendAsync<TransCreateOutput, TransCreateInput>(HttpMethod.Post, "twc.notary.trans.create", input);
    }

    public async Task<TransGetOutput> GetTransAsync(TransGetInput input)
    {
        //var res = await new AntChainNotaryTestService(Config).GetTransAsync(input);
        return await SendAsync<TransGetOutput, TransGetInput>(HttpMethod.Post, "twc.notary.trans.get", input);
    }

    public async Task<FileCreateOutput> CreateFileAsync(FileCreateInput input)
    {
        return await SendAsync<FileCreateOutput, FileCreateInput>(HttpMethod.Post, "twc.notary.file.create", input);
    }

    public async Task<FileGetOutput> GetFileAsync(FileGetInput input)
    {
        return await SendAsync<FileGetOutput, FileGetInput>(HttpMethod.Post, "twc.notary.file.get", input);
    }

    public async Task<CertificateDetailGetOutput> GetCertificateDetailAsync(CertificateDetailGetInput input)
    {
        return await SendAsync<CertificateDetailGetOutput, CertificateDetailGetInput>(HttpMethod.Post, "twc.notary.certificate.detail.get", input);
    }

    protected async Task<TOutput> SendAsync<TOutput, TInput>(HttpMethod method, string action, TInput input)
        where TOutput : BaseOutput
        where TInput : BaseInput
    {
        var config = await GetConfigAsync();
        input.ProductInstanceId ??= config.InstanceId;
        var body = Utils.ObjectUtls.ToSortedDictionary(input);
        var output = await SendAsync<TOutput>(config, method, action, body);
        output.ProductInstanceId = input.ProductInstanceId;
        return output;
    }

    async Task<TOutput> SendAsync<TOutput>(AntChainNotaryConfig config, HttpMethod method, string action, SortedDictionary<string, string?> body, SortedDictionary<string, string?>? query = null, SortedDictionary<string, string?>? headers = null)
        where TOutput : BaseOutput
    {
        var time = DateTime.UtcNow;
        var timestamp = time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);

        query ??= new SortedDictionary<string, string?>(StringComparer.Ordinal);
        query.Add("method", action);
        query.Add("version", "1.0");
        query.Add("sign_type", "HmacSHA1");
        query.Add("req_time", timestamp);
        query.Add("req_msg_id", Guid.NewGuid().ToString("N"));
        query.Add("access_key", config.AccessKeyId);
        query.Add("_prod_code", "TWC");
        query.Add("_prod_channel", ""); // undefined

        headers ??= new SortedDictionary<string, string?>(StringComparer.Ordinal);
        headers.Add("host", config.Host[8..]);

        var signParas = new SortedDictionary<string, string?>(body);
        foreach (var kvp in query)
        {
            signParas.Add(kvp.Key, kvp.Value);
        }
        var dataToSign = Utils.StringUtils.ToQueryString(signParas, false, true, true);
        Console.WriteLine("dataToSign: {0}", dataToSign);
        var sign = ComputeSignature(dataToSign, config.AccessKeySecret);
        query.Add("sign", sign);
        var queryString = Utils.StringUtils.ToQueryString(query, false, true);

        var client = CreateHttpClient(config.Host);
        client.Timeout = TimeSpan.FromMinutes(30);

        var request = new HttpRequestMessage(method, $"gateway.do?{queryString}");
        foreach (var header in headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
        request.Content = new FormUrlEncodedContent(body);

        var res = await SendAsync<Output<TOutput>>(client, request);
        return EnsureSuccess(res);
    }

    T EnsureSuccess<T>(Output<T> result)
        where T : BaseOutput
    {
        var response = result.Response ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
        response.EnsureResult();
        return response;
    }

    static string ComputeSignature(string data, string accessKeySecret)
    {
        using var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(accessKeySecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }
}
