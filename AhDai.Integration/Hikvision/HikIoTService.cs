using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Hikvision.Configs;
using AhDai.Integration.Hikvision.Models.IoT;
using AhDai.Integration.Hikvision.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// HikIoTService
/// </summary>
[Attributes.Service()]
internal class HikIoTService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IHikIoTConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseHikvisionService<HikIoTConfig, IHikIoTConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IHikIoTService
{
    protected override string ServiceName => "海康互联";

    HttpClient CreateAppHttpClient(string host, string appAccessToken)
    {
        var client = CreateHttpClient(host);
        //client.DefaultRequestHeaders.Remove("App-Access-Token");
        client.DefaultRequestHeaders.Add("App-Access-Token", appAccessToken);
        return client;
    }

    HttpClient CreateUserHttpClient(string host, AccessContext context)
    {
        var client = CreateAppHttpClient(host, context.AppAccessToken);
        //client.DefaultRequestHeaders.Remove("User-Access-Token");
        client.DefaultRequestHeaders.Add("User-Access-Token", context.UserAccessToken);
        return client;
    }

    #region 身份及授权

    public async Task<AppAccessTokenOutput> GetAppAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"HikIoT:{config.AppKey}:AppAccessToken");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var data = JsonUtil.Deserialize<AppAccessTokenOutput>(value.ToString());
                return data ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var data = await GetAppAccessTokenAsync(false);
                value = JsonUtil.Serialize(data);
                await rdb.StringSetAsync(key, value, TimeSpan.FromSeconds(data.ExpiresIn * 60 - 30));
                return data;
            }
        }
        var url = $"auth/exchangeAppToken";
        var res = await PostAsync<Output<AppAccessTokenOutput>>(url, new { appKey = config.AppKey, appSecret = config.AppSecret });
        return EnsureSuccess(res);
    }

    public async Task<AuthCodeOutput> GetAuthCodeAsync(AuthCodeInput input)
    {
        var config = await GetConfigAsync();
        if (string.IsNullOrEmpty(input.UserName))
        {
            input.UserName = config.Username;
            input.Password = config.Password;
        }
        var url = $"artemis/api/v1/oauth/authorize";
        var res = await PostAsync<Output<AuthCodeOutput>>(url, input);
        return EnsureSuccess(res);
    }

    public async Task<UserAccessTokenOutput> GetUserAccessTokenAsync(string appAccessToken, UserAccessTokenInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateAppHttpClient(config.Host, appAccessToken);
        var url = $"auth/third/code2Token";
        var res = await GetEncryptedAsync<UserAccessTokenOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    public async Task<UserAccessTokenOutput> RefreshUserAccessTokenAsync(string appAccessToken, RefreshUserAccessTokenInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateAppHttpClient(config.Host, appAccessToken);
        var url = $"auth/third/refreshUserAccessToken";
        var res = await PostEncryptedAsync<UserAccessTokenOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    #endregion

    #region 视频 - 取流/预览/对讲

    public async Task<DeviceTokenOutput> GetDeviceTokenAsync(AccessContext context, DeviceTokenInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/token/device/get";
        var res = await GetEncryptedAsync<DeviceTokenOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    public async Task<DeviceTokenOutput[]> GetDeviceTokensAsync(AccessContext context, DeviceTokenInput[] inputs)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/token/device/batch";
        var res = await PostEncryptedAsync<DeviceTokenOutput[]>(config, client, url, inputs);
        return EnsureSuccess(res);
    }

    public async Task<OpsTokenOutput> GetOpsTokenAsync(AccessContext context)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/token/ops/get";
        var res = await GetEncryptedAsync<OpsTokenOutput>(config, client, url);
        return EnsureSuccess(res);
    }

    public async Task<StreamingMediaAttrsOutput> GetStreamingMediaAttrsAsync(AccessContext context, StreamingMediaAttrsInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/direct/v1/streamingMedia/getAttrs";
        var res = await PostEncryptedAsync<StreamingMediaAttrsOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    #endregion

    #region 视频 - 通道

    public async Task<PageOutput<CameraOutput>> PageCameraAsync(AccessContext context, PageInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/camera/v1/page";
        var res = await GetEncryptedAsync<CameraOutput[]>(config, client, url, input);
        var data = EnsureSuccess(res);
        return new PageOutput<CameraOutput>(data, res.Count);
    }

    #endregion

    #region 硬件设备 - 设备/通道

    public async Task<PageOutput<DeviceOutput>> PageDeviceAsync(AccessContext context, PageInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/page";
        var res = await GetEncryptedAsync<DeviceOutput[]>(config, client, url, input);
        var data = EnsureSuccess(res);
        return new PageOutput<DeviceOutput>(data, res.Count);
    }

    public async Task<DeviceCapacitiesOutput> GetDeviceCapacitiesAsync(AccessContext context, DeviceCapacitiesQueryInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/getDeviceCapacities";
        var res = await GetEncryptedAsync<DeviceCapacitiesOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    public async Task<DeviceResourceOutput> GetDeviceResourceAsync(AccessContext context, DeviceResourceQueryInput input)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/desk/pc/resource/v1/getById";
        var res = await GetEncryptedAsync<DeviceResourceOutput>(config, client, url, input);
        return EnsureSuccess(res);
    }

    #endregion


    protected async Task<Output<TData>> GetEncryptedAsync<TData>(HikIoTConfig config, HttpClient client, string url, object? query = null)
    {
        if (config.EnableSecret)
        {
            if (query != null)
            {
                var queryString = Utils.StringUtils.ObjectToQueryString(query, true);
                if (!string.IsNullOrEmpty(queryString))
                {
                    var encrypted = HikIoTRsaHelper.EncryptWithPrivateKey(config.AppSecret, queryString);
                    url += $"?querySecret={HttpUtility.UrlEncode(encrypted)}";
                }
            }
            var res = await GetAsync<Output<string>>(client, url);
            var data = DeserializeSecretData<TData>(config, res.Data);
            return ConvertOutput(res, data);
        }
        return await GetAsync<Output<TData>>(client, url, query);
    }

    protected async Task<Output<TData>> PostEncryptedAsync<TData>(HikIoTConfig config, HttpClient client, string url, object? body)
    {
        if (config.EnableSecret)
        {
            var dict = new Dictionary<string, string>();
            if (body != null)
            {
                var bodyJson = JsonUtil.Serialize(body);
                var encrypted = HikIoTRsaHelper.EncryptWithPrivateKey(config.AppSecret, bodyJson);
                dict["bodySecret"] = encrypted;
            }
            var res = await PostAsync<Output<string>>(client, url, dict);
            var data = DeserializeSecretData<TData>(config, res.Data);
            return ConvertOutput(res, data);
        }
        return await PostAsync<Output<TData>>(client, url, body);
    }

    T DeserializeSecretData<T>(HikIoTConfig config, string? encryptedData)
    {
        if (string.IsNullOrEmpty(encryptedData)) throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");

        var json = HikIoTRsaHelper.DecryptWithPrivateKey(config.AppSecret, encryptedData);
        if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("解密结果：{json}", json);
        return JsonUtil.Deserialize<T>(json) ?? throw new Exception($"请求{ServiceName}返回数据反序列化失败，请联系管理员");
    }

    T EnsureSuccess<T>(Output<T> result)
    {
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }

    static Output<TData> ConvertOutput<TData>(Output<string> source, TData data)
        => new()
        {
            Code = source.Code,
            Msg = source.Msg,
            Data = data,
            Count = source.Count,
        };
}
