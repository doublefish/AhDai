using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.ESign.Configs;
using AhDai.Integration.ESign.Models;
using AhDai.Integration.ESign.Providers;
using AhDai.Integration.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Integration.ESign;

/// <summary>
/// ESignService
/// </summary>
[Attributes.Service()]
internal class ESignService(IESignConfigProvider configProvider, IHttpClientFactory httpClientFactory
    , IBaseFileService baseFileService)
    : BaseService<ESignConfig, IESignConfigProvider>(configProvider, httpClientFactory)
    , IESignService
{
    readonly IBaseFileService _baseFileService = baseFileService;

    protected override string ServiceName => "电子签";


    #region 基础
    public async Task<OrgOutput> GetOrgByNameAsync(string orgName)
    {
        var url = $"v3/organizations/identity-info?orgName={orgName}";
        var res = await GetAsync<Output<OrgOutput>>(url);
        return EnsureSuccess(res);
    }

    public async Task<PersonOutput> GetPersonByAccountAsync(string psnAccount)
    {
        var url = $"v3/persons/identity-info?psnAccount={psnAccount}";
        var res = await GetAsync<Output<PersonOutput>>(url);
        return EnsureSuccess(res);
    }
    #endregion

    #region 授权
    public async Task<OrgAuthUrlOutput> GetOrgAuthUrlAsync(object input)
    {
        var url = $"v3/org-auth-url";
        var res = await PostAsync<Output<OrgAuthUrlOutput>>(url, input);
        return EnsureSuccess(res);
    }
    #endregion

    #region 文件
    public async Task<FileUploadUrlOutput> GetFileUploadUrlAsync(FileUploadUrlInput input)
    {
        var url = $"v3/files/file-upload-url";
        var res = await PostAsync<Output<FileUploadUrlOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task UploadFileToUrlAsync(string url, Stream stream, byte[]? contentMD5 = null, string? contentType = null)
    {
        contentType ??= "application/octet-stream";

        var finalMd5 = contentMD5;
        if (finalMd5 == null)
        {
            stream.Seek(0, SeekOrigin.Begin);
            finalMd5 = await MD5.HashDataAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }

        var content = new StreamContent(stream);
        content.Headers.ContentLength = stream.Length;
        content.Headers.ContentMD5 = finalMd5;
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        //var config = await GetConfigAsync();
        //var client = CreateHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = content
        };
        await SendAsync<FileUploadOutput>(null, request);
    }

    public async Task<string> UploadFileAsync(FileUploadInput input)
    {
        var contentType = input.ContentType ?? "application/octet-stream";
        var filePath = input.FilePath;
        var fileStream = input.FileStream;
        var isTempPath = false;
        var isTempStream = false;
        try
        {
            if (fileStream == null)
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileStream = File.OpenRead(filePath);
                    isTempStream = true;
                }
                else if (!string.IsNullOrEmpty(input.FileUrl))
                {
                    var (fileData, fs) = await _baseFileService.DownloadAndOpenAsync(null, AppContext.BaseDirectory, DateTime.Today.ToString("yyyy-MM-dd"), input.FileUrl, input.FileName);
                    filePath = fileData.ActualPath;
                    fileStream = fs;
                    isTempPath = true;
                    isTempStream = true;
                }
                else
                {
                    throw new ArgumentException("文件链接和地址不可同时为空");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(filePath)) throw new ArgumentException("文件地址不可同时为空");
            }

            var fileName = input.FileName ?? Path.GetFileName(filePath);
            var contentMd5 = await MD5.HashDataAsync(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);
            var res = await GetFileUploadUrlAsync(new FileUploadUrlInput()
            {
                ContentMd5 = Convert.ToBase64String(contentMd5),
                ContentType = contentType,
                FileName = fileName,
                FileSize = fileStream.Length,
                ConvertToPDF = input.ConvertToPDF,
                ConvertToHTML = input.ConvertToHTML,
                DedicatedCloudId = input.DedicatedCloudId,
            });
            await UploadFileToUrlAsync(res.FileUploadUrl, fileStream, contentMd5, contentType);
            return res.FileId;
        }
        finally
        {
            if (isTempStream && fileStream != null)
            {
                fileStream.Dispose();
            }
            if (isTempPath && File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    public async Task<FileInfoOutput> GetFileAsync(string fileId, bool pageSize = false)
    {
        var url = $"v3/files/{fileId}?pageSize={pageSize}";
        var res = await GetAsync<Output<FileInfoOutput>>(url);
        return EnsureSuccess(res);
    }

    public async Task<FileInfoOutput> GetFileUntilReadyAsync(string fileId, int maxRetryCount = 6)
    {
        for (var i = 0; i < maxRetryCount; i++)
        {
            var docFile = await GetFileAsync(fileId);
            if (docFile.FileStatus == 2 || docFile.FileStatus == 5)
            {
                return docFile;
            }
            if (i < 5)
            {
                if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("等待电子签文件转换完成：{i}", i);
                await Task.Delay(500);
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("等待电子签文件转换完成失败：{i}", i);
            }
        }
        throw new Exception("等待电子签文件转换完成失败");
    }

    public async Task<GetFileKeywordPositionOutput[]> GetFileKeywordPositionsAsync(string fileId, params string[] keywords)
    {
        var dict = new Dictionary<string, string[]>()
        {
            ["keywords"] = keywords
        };
        var url = $"v3/files/{fileId}/keyword-positions";
        var res = await PostAsync<Output<GetFileKeywordPositionsOutput>>(url, dict);
        var data = EnsureSuccess(res);
        return data.KeywordPositions;
    }
    #endregion

    #region 模板
    public async Task<DocTemplateCreateUrlOutput> GetDocTemplateCreateUrlAsync(DocTemplateCreateUrlInput input)
    {
        var url = $"v3/doc-templates/doc-template-create-url";
        var res = await PostAsync<Output<DocTemplateCreateUrlOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task<DocTemplateEditUrlOutput> GetDocTemplateEditUrlAsync(string docTemplateId, DocTemplateEditUrlInput input)
    {
        var url = $"v3/doc-templates/{docTemplateId}/doc-template-edit-url";
        var res = await PostAsync<Output<DocTemplateEditUrlOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task<IDictionary<string, object>> GetDocTemplateAsync(string docTemplateId)
    {
        var url = $"v3/doc-templates/{docTemplateId}";
        var res = await GetAsync<Output<IDictionary<string, object>>>(url);
        return EnsureSuccess(res);
    }

    public async Task<FileCreateByDocTemplateOutput> CreateFileByDocTemplateAsync(FileCreateByDocTemplateInput input)
    {
        var url = $"v3/files/create-by-doc-template";
        var res = await PostAsync<Output<FileCreateByDocTemplateOutput>>(url, input);
        return EnsureSuccess(res);
    }
    #endregion

    #region 流程
    public async Task<string> CreateSignFlowByFileAsync(SignFlowCreateByFileInput input)
    {
        var url = $"v3/sign-flow/create-by-file";
        var res = await PostAsync<Output<SignFlowCreateByFileOutput>>(url, input);
        var data = EnsureSuccess(res);
        if (string.IsNullOrEmpty(data.SignFlowId)) throw new Exception($"请求{ServiceName}未返回SignFlowId，请联系管理员");
        return data.SignFlowId;
    }

    public async Task RevokeSignFlowAsync(string signFlowId, SignFlowRevokeInput input)
    {
        var url = $"v3/sign-flow/{signFlowId}/revoke";
        var res = await PostAsync<Output<IDictionary<string, string>>>(url, input);
        EnsureSuccess(res);
    }

    public async Task UrgeSignFlowAsync(string signFlowId, SignFlowUrgeInput input)
    {
        var url = $"v3/sign-flow/{signFlowId}/urge";
        await PostAsync<Output<object>>(url, input);
    }

    public async Task<SignFlowRescissionOutput> InitiateSignFlowRescissionAsync(string signFlowId, SignFlowRescissionInput input)
    {
        if (input.RescindFileList == null || input.RescissionInitiator == null)
        {
            var detail = await GetSignFlowDetailAsync(signFlowId);
            input.RescindFileList ??= detail.Docs?.Select(x => x.FileId ?? "").ToArray() ?? [];
            input.RescissionInitiator ??= new SignFlowInitiatorInput();
            if (detail.SignFlowInitiator?.PsnInitiator != null)
            {
                input.RescissionInitiator.PsnInitiator = new SignFlowPsnInitiatorInput()
                {
                    PsnId = detail.SignFlowInitiator.PsnInitiator.PsnId,
                };
            }
            if (detail.SignFlowInitiator?.OrgInitiator != null)
            {
                input.RescissionInitiator.OrgInitiator = new SignFlowOrgInitiatorInput()
                {
                    OrgId = detail.SignFlowInitiator.OrgInitiator.OrgId,
                    Transactor = new SignFlowTransactorInput()
                    {
                        PsnId = detail.SignFlowInitiator.OrgInitiator.Transactor?.PsnId,
                    },
                };
            }
        }
        input.SignFlowConfig ??= new
        {
            noticeConfig = new
            {
                noticeTypes = "1",
            },
            notifyUrl = "",
        };

        var config = await GetConfigAsync();
        var url = $"v3/sign-flow/{signFlowId}/initiate-rescission";
        var res = await PostAsync<Output<SignFlowRescissionOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task<SignFlowDetailOutput> GetSignFlowDetailAsync(string signFlowId)
    {
        var url = $"v3/sign-flow/{signFlowId}/detail";
        var res = await GetAsync<Output<SignFlowDetailOutput>>(url);
        var data = EnsureSuccess(res);
        data.SignFlowId = signFlowId;
        return data;
    }

    public async Task<SignFlowSignUrlOutput> GetSignFlowSignUrlAsync(string signFlowId, SignFlowSignUrlInput input)
    {
        var url = $"v3/sign-flow/{signFlowId}/sign-url";
        var res = await PostAsync<Output<SignFlowSignUrlOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task<SignFileDownloadOutput> GetSignFlowFileDownloadUrlAsync(string signFlowId)
    {
        var url = $"v3/sign-flow/{signFlowId}/file-download-url";
        var res = await GetAsync<Output<SignFileDownloadOutput>>(url);
        return EnsureSuccess(res);
    }

    public async Task<SignFlowNotifyOutput> GetSignFlowNotifyResultAsync(HttpContext httpContext)
    {
        if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("请求头：{Headers}", JsonUtil.Serialize(httpContext.Request.Headers));
        var appId = httpContext.Request.Headers["X-Tsign-Open-App-Id"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到AppId");
        var signature = httpContext.Request.Headers["X-Tsign-Open-SIGNATURE"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到签名");
        var timestamp = httpContext.Request.Headers["X-Tsign-Open-TIMESTAMP"].FirstOrDefault() ?? throw new ArgumentException("验签失败：未读取到时间戳");
        httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
        using var requestReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
        var body = await requestReader.ReadToEndAsync();
        if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("请求体：{Body}", body);
        var sorted = new SortedDictionary<string, string>();
        foreach (var kvp in httpContext.Request.Query)
        {
            sorted.Add(kvp.Key, kvp.Value.ToString());
        }
        var query = string.Join("", sorted.Values);

        var config = await GetConfigAsync();
        if (appId != config.AppId) throw new ArgumentException("验签失败：AppId不一致");
        VerifySignature(timestamp + query + body, signature, config.AppSecret);
        var output = JsonUtil.Deserialize<SignFlowNotifyOutput>(body) ?? throw new ArgumentException("反序列化失败");
        return output;
    }
    #endregion

    protected override async Task<TOutput> SendAsync<TOutput>(HttpClient? client, HttpMethod method, string url, object? data, CancellationToken cancellationToken = default)
    {
        var config = await GetConfigAsync();
        var request = new HttpRequestMessage(method, url);

        var contentType = "";
        var contentMD5 = "";
        if (method != HttpMethod.Get && method != HttpMethod.Delete && data != null)
        {
            var body = JsonUtil.Serialize(data);
            var contentMD5Bytes = MD5.HashData(Encoding.UTF8.GetBytes(body));
            contentMD5 = Convert.ToBase64String(contentMD5Bytes);

            var content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json", "UTF-8");
            content.Headers.ContentMD5 = contentMD5Bytes;
            request.Content = content;
            contentType = content.Headers.ContentType.ToString();
        }

        var accept = "*/*";
        var dataToSign = new StringBuilder().Append(method.Method.ToUpper()).Append('\n')
            .Append(accept).Append('\n')
            .Append(contentMD5).Append('\n')
            .Append(contentType).Append('\n')
            .Append("").Append('\n')
            .Append('/').Append(url).ToString();
        var signature = ComputeSignature(dataToSign, config.AppSecret);

        request.Headers.Add("Accept", accept);
        request.Headers.Add("X-Tsign-Open-App-Id", config.AppId);
        request.Headers.Add("X-Tsign-Open-Auth-Mode", "Signature");
        request.Headers.Add("X-Tsign-Open-Ca-Signature", signature);
        request.Headers.Add("X-Tsign-Open-Ca-Timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());

        client = CreateHttpClient(config.Host);
        // E签宝好像会读取所有头用来签名
        client.DefaultRequestHeaders.Clear();
        return await SendAsync<TOutput>(client, request, cancellationToken);
    }

    static string ComputeSignature(string data, string appSecret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }

    static void VerifySignature(string data, string signature, string appSecret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        var builder = new StringBuilder();
        foreach (var b in hash)
        {
            builder.Append(b.ToString("X2"));
        }
        var signature2 = builder.ToString().ToLower();
        if (signature2 != signature)
        {
            throw new ArgumentException("验签失败");
        }
    }

    T EnsureSuccess<T>(Output<T> result)
    {
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }
}
