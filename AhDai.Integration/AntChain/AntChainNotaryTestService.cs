using AhDai.Core.Utils;
using AlibabaCloud.TeaUtil.Models;
using AntChain.AlipayUtil;
using AntChain.SDK.TWC;
using AntChain.SDK.TWC.Models;
using AhDai.ExternalService.AntChain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;

namespace AhDai.Integration.AntChain;

internal class AntChainNotaryTestService(AntChainNotaryConfig config)
{
    readonly ILogger _logger = LoggerUtil.GetLogger(typeof(AntChainNotaryTestService).FullName ?? typeof(AntChainNotaryTestService).Name);
    readonly AntChainNotaryConfig _config = config;

    protected Client GetClient() => new(new Config()
    {
        AccessKeyId = _config.AccessKeyId,
        AccessKeySecret = _config.AccessKeySecret,
    });

    public async Task<TwcNotaryTransCreateOutput> CreateTransAsync(TwcNotaryTransCreateInput input)
    {
        if (input.Customer == null) throw new ArgumentException("关联实体的身份识别信息不可为空");


        var request = new CreateTransRequest()
        {
            ProductInstanceId = input.ProductInstanceId,
            Customer = new Identity
            {
                UserType = input.Customer.UserType,
                CertType = input.Customer.CertType,
                CertName = input.Customer.CertName,
                CertNo = input.Customer.CertNo,
                MobileNo = input.Customer.MobileNo,
                LegalPerson = input.Customer.LegalPerson,
                LegalPersonId = input.Customer.LegalPersonId,
                LegalPersonCertType = input.Customer.LegalPersonCertType,
                Agent = input.Customer.Agent,
                AgentId = input.Customer.AgentId,
                AgentCertType = input.Customer.AgentCertType,
            },
            Properties = input.Properties,
        };
        var runtime = new RuntimeOptions()
        {
            HttpProxy = "http://127.0.0.1:8888",
            NoProxy = "localhost,127.0.0.1",
        };

        //var client = GetClient();
        //var resp = await client.CreateTransExAsync(request, [], runtime);
        var resp = await CreateTransExAsync(request, [], runtime);
        _logger.LogInformation("请求蚂蚁链存证服务结果=>{resp}", JsonUtil.Serialize(resp));
        if (resp.ResultCode != "200") throw new Exception($"请求蚂蚁链存证服务发生异常：[{resp.ResultCode}]{resp.ResultMsg}，请联系管理员");
        return new TwcNotaryTransCreateOutput()
        {
            ResultCode = resp.ResultCode,
            ResultMsg = resp.ResultMsg,
            ReqMsgId = resp.ReqMsgId,
            TransactionId = resp.TransactionId,
        };
    }

    public async Task<TwcNotaryTransGetOutput> GetTransAsync(TwcNotaryTransGetInput input)
    {
        var request = new GetTransRequest()
        {
            ProductInstanceId = input.ProductInstanceId,
            TransactionId = input.TransactionId,
        };
        var runtime = new RuntimeOptions()
        {
            HttpProxy = "http://127.0.0.1:8888",
            NoProxy = "localhost,127.0.0.1",
        };

        //var client = GetClient();
        //var resp = await client.GetTransExAsync(request, [], runtime);
        var resp = await GetTransExAsync(request, [], runtime);
        _logger.LogInformation("请求蚂蚁链存证服务结果=>{resp}", JsonUtil.Serialize(resp));
        if (resp.ResultCode != "200") throw new Exception($"请求蚂蚁链存证服务发生异常：[{resp.ResultCode}]{resp.ResultMsg}，请联系管理员");
        return new TwcNotaryTransGetOutput()
        {
            ResultCode = resp.ResultCode,
            ResultMsg = resp.ResultMsg,
            ReqMsgId = resp.ReqMsgId,
            TransactionId = resp.TransactionId,
            FileUrl = [.. resp.FileUrl],
        };
    }

    protected async Task<CreateTransResponse> CreateTransExAsync(CreateTransRequest request, Dictionary<string, string> headers, RuntimeOptions runtime)
    {
        AlibabaCloud.TeaUtil.Common.ValidateModel(request);
        return TeaModel.ToObject<CreateTransResponse>(await DoRequestAsync("1.0", "twc.notary.trans.create", "HTTPS", "POST", "/gateway.do", request.ToMap(), headers, runtime));
    }

    protected async Task<GetTransResponse> GetTransExAsync(GetTransRequest request, Dictionary<string, string> headers, RuntimeOptions runtime)
    {
        AlibabaCloud.TeaUtil.Common.ValidateModel(request);
        return TeaModel.ToObject<GetTransResponse>(await DoRequestAsync("1.0", "twc.notary.trans.get", "HTTPS", "POST", "/gateway.do", request.ToMap(), headers, runtime));
    }

    async Task<Dictionary<string, object>> DoRequestAsync(string version, string action, string protocol, string method, string pathname, Dictionary<string, object> request, Dictionary<string, string> headers, RuntimeOptions runtime)
    {
        var runtime_ = new Dictionary<string, object?>
        {
            { "timeouted", "retry" },
            {
                "readTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ReadTimeout, 20000)
            },
            {
                "connectTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ConnectTimeout, 20000)
            },
            {
                "httpProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpProxy, "")
            },
            {
                "httpsProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpsProxy, "")
            },
            {
                "noProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.NoProxy, "")
            },
            {
                "maxIdleConns", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxIdleConns, 60000)
            },
            { "maxIdleTimeMillis", 5 },
            { "keepAliveDuration", 5000 },
            { "maxRequests", 100 },
            { "maxRequestsPerHost", 100 },
            {
                "retry",
                new Dictionary<string, object?>
                {
                    { "retryable", runtime.Autoretry },
                    {
                        "maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)
                    }
                }
            },
            {
                "backoff",
                new Dictionary<string, object?>
                {
                    {
                        "policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")
                    },
                    {
                        "period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)
                    }
                }
            },
            { "ignoreSSL", runtime.IgnoreSSL }
        };
        TeaRequest? _lastRequest = null;
        Exception? innerException = null;
        int _retryTimes = 0;
        while (_retryTimes < 3)
        {
            if (_retryTimes > 0)
            {
                Thread.Sleep(1000);
            }
            _retryTimes++;
            try
            {
                var teaRequest = new TeaRequest()
                {
                    Protocol = protocol,
                    Method = method,
                    Pathname = pathname,
                    Query = new Dictionary<string, string>()
                    {
                        { "method", action },
                        { "version", version },
                        { "sign_type", "HmacSHA1" },
                        { "req_time", AntchainUtils.GetTimestamp() },
                        { "req_msg_id", AntchainUtils.GetNonce() },
                        { "access_key", _config.AccessKeyId },
                        //{ "base_sdk_version", "TeaSDK-2.0" },
                        { "sdk_version", "1.13.8" },
                        { "_prod_code", "TWC" },
                        { "_prod_channel", "undefined" }
                    },
                    //if (!AlibabaCloud.TeaUtil.Common.Empty(_securityToken))
                    //{
                    //    teaRequest.Query["security_token"] = _securityToken;
                    //}

                    Headers = TeaConverter.merge<string>(
                    [
                        new Dictionary<string, string>()
                        {
                            { "host", AlibabaCloud.TeaUtil.Common.DefaultString("", "twc-openapi.antchain.antgroup.com") },
                            { "user-agent", AlibabaCloud.TeaUtil.Common.GetUserAgent("") }
                        },
                        headers
                    ])
                };

                var sorted = new SortedDictionary<string, string>(teaRequest.Query, StringComparer.Ordinal);
                teaRequest.Query = sorted.ToDictionary();

                var map = AlibabaCloud.TeaUtil.Common.AnyifyMapValue(AlibabaCloud.Commons.Common.Query(request));
                map = new SortedDictionary<string, object>(map, StringComparer.Ordinal).ToDictionary();

                teaRequest.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToFormString(map));
                teaRequest.Headers["content-type"] = "application/x-www-form-urlencoded";
                var signedParams = TeaConverter.merge<string>([teaRequest.Query, AlibabaCloud.Commons.Common.Query(request)]);
                var dataToSign = BuildStringToSign(signedParams);
                Console.WriteLine("dataToSign：{0}", dataToSign);
                teaRequest.Query["sign"] = AntchainUtils.GetSignature(signedParams, _config.AccessKeySecret);
                Console.WriteLine("sign={0}", teaRequest.Query["sign"]);
                _lastRequest = teaRequest;
                string text = AlibabaCloud.TeaUtil.Common.ReadAsString((await TeaCore.DoActionAsync(teaRequest, runtime_)).Body);
                var dictionary = AlibabaCloud.TeaUtil.Common.AssertAsMap(AlibabaCloud.TeaUtil.Common.AssertAsMap(AlibabaCloud.TeaUtil.Common.ParseJSON(text)).Get("response"));
                if (AntchainUtils.HasError(text, _config.AccessKeySecret))
                {
                    throw new TeaException(new Dictionary<string, object?>
                    {
                        { "message", dictionary.Get("result_msg") },
                        { "data", dictionary },
                        { "code", dictionary.Get("result_code") }
                    });
                }

                return dictionary;
            }
            catch (Exception ex)
            {
                if (TeaCore.IsRetryable(ex))
                {
                    innerException = ex;
                    continue;
                }
                throw;
            }
        }

        throw new TeaUnretryableException(_lastRequest, innerException);
    }

    static string BuildStringToSign(Dictionary<string, string> signedParam)
    {
        _ = string.Empty;
        var dictionary = new Dictionary<string, string>();
        foreach (var item in signedParam)
        {
            if (!string.IsNullOrEmpty(item.Value))
            {
                dictionary.Add(item.Key, item.Value);
            }
        }
        return GetUrlFormedMap(dictionary);
    }

    internal static string GetUrlFormedMap(Dictionary<string, string> source)
    {
        var list = source.Keys.ToList();
        list.Sort();
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            string text = list[i];
            if (i > 0)
            {
                stringBuilder.Append('&');
            }

            stringBuilder.Append(PercentEncode(text)).Append('=').Append(PercentEncode(source[text]));
        }

        return stringBuilder.ToString();
    }

    internal static string? PercentEncode(string value)
    {
        if (value == null)
        {
            return null;
        }

        var stringBuilder = new StringBuilder();
        string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        for (int i = 0; i < bytes.Length; i++)
        {
            char c = (char)bytes[i];
            if (text.Contains(c))
            {
                stringBuilder.Append(c);
            }
            else
            {
                stringBuilder.Append('%').Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)c));
            }
        }

        return stringBuilder.ToString().Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
    }
}
