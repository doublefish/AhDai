using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Hikvision.Configs;
using AhDai.Integration.Hikvision.Models.IoT;
using AhDai.Integration.Hikvision.Providers;
using System;
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
        client.DefaultRequestHeaders.Add("App-Access-Token", appAccessToken);
        return client;
    }

    HttpClient CreateUserHttpClient(string host, AccessContext context)
    {
        var client = CreateAppHttpClient(host, context.AppAccessToken);
        client.DefaultRequestHeaders.Add("User-Access-Token", context.UserAccessToken);
        return client;
    }


    public async Task<AppAccessTokenOutput> GetAppAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"HikIoT:{config.AppKey}:AccessToken");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<AppAccessTokenOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetAppAccessTokenAsync(false);
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, TimeSpan.FromSeconds(res.ExpiresIn * 60 - 30));
                return res;
            }
        }
        var url = $"auth/exchangeAppToken";
        var result = await PostAsync<Output<AppAccessTokenOutput>>(url, new { appKey = config.AppKey, appSecret = config.AppSecret });
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
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
        var result = await PostAsync<Output<AuthCodeOutput>>(url, input);
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }

    public async Task<UserAccessTokenOutput> GetUserAccessTokenAsync(string appAccessToken, string authCode)
    {
        var config = await GetConfigAsync();
        var url = $"auth/third/code2Token?authCode={authCode}";
        var client = CreateAppHttpClient(config.Host, appAccessToken);
        var result = await GetAsync<Output<UserAccessTokenOutput>>(client, url);
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }

    public async Task<UserAccessTokenOutput> RefreshUserAccessTokenAsync(string appAccessToken, string userAccessToken, string refreshUserToken)
    {
        var config = await GetConfigAsync();
        var url = $"auth/third/refreshUserAccessToken";
        var client = CreateAppHttpClient(config.Host, appAccessToken);
        var result = await PostAsync<Output<UserAccessTokenOutput>>(client, url, new { userAccessToken, refreshUserToken });
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }

    public async Task<DeviceOutput[]> PageDeviceAsync(AccessContext context, int page = 1, int size = 20)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/v1/page";
        var result = await GetSecretAsync<Output<DeviceOutput[]>>(client, url, new { page, size });
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }

    public async Task<CameraOutput[]> PageCameraAsync(AccessContext context, int page = 1, int size = 20)
    {
        var config = await GetConfigAsync();
        var client = CreateUserHttpClient(config.Host, context);
        var url = $"device/camera/v1/page";
        var result = await GetSecretAsync<Output<CameraOutput[]>>(client, url, new { page, size });
        return result.Data ?? throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");
    }


    protected async Task<TOutput> GetSecretAsync<TOutput>(HttpClient client, string url, object query)
        where TOutput : IBaseOutput
    {
        var config = await GetConfigAsync();
        if (config.EnableSecret)
        {
            var queryString = Utils.StringUtils.ObjectToQueryString(query, true);
            var encrypted = HikIoTRsaHelper.EncryptWithPrivateKey(config.AppSecret, queryString);
            //var encrypted = HikIoTRsaHelper2.EncryptWithPrivateKey(config.AppSecret, queryString);
            url += $"?querySecret={HttpUtility.UrlEncode(encrypted)}";
            var result = await GetAsync<Output<string>>(client, url);
            if (string.IsNullOrWhiteSpace(result.Data)) throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");

            //var json = HikIoTRsaHelper2.DecryptWithPrivateKey(config.AppSecret, result.Data);
            var json = HikIoTRsaHelper.DecryptWithPrivateKey(config.AppSecret, result.Data);
            return JsonUtil.Deserialize<TOutput>(json) ?? throw new Exception($"请求{ServiceName}返回数据反序列化失败，请联系管理员");
        }
        return await GetAsync<TOutput>(client, url, query);
    }

    protected async Task<TOutput> PostSecretAsync<TOutput>(HttpClient client, string url, object body)
        where TOutput : IBaseOutput
    {
        var config = await GetConfigAsync();
        if (config.EnableSecret)
        {
            var bodyJson = JsonUtil.Serialize(body);
            //var encrypted = HikIoTRsaHelper2.EncryptWithPrivateKey(config.AppSecret, bodyJson);
            var encrypted = HikIoTRsaHelper.EncryptWithPrivateKey(config.AppSecret, bodyJson);
            var result = await PostAsync<Output<string>>(client, url, new { data = encrypted });

            if (string.IsNullOrWhiteSpace(result.Data)) throw new Exception($"请求{ServiceName}返回数据为空，请联系管理员");

            //var json = HikIoTRsaHelper2.DecryptWithPrivateKey(config.AppSecret, result.Data);
            var json = HikIoTRsaHelper.DecryptWithPrivateKey(config.AppSecret, result.Data);
            return JsonUtil.Deserialize<TOutput>(json) ?? throw new Exception($"请求{ServiceName}返回数据反序列化失败，请联系管理员");
        }
        return await PostAsync<TOutput>(client, url, body);
    }
}
