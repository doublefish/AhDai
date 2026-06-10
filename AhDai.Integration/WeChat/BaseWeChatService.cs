using AhDai.Core.Interfaces.Services;
using AhDai.Core.Utils;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// BaseWeChatService
/// </summary>
internal abstract class BaseWeChatService<TConfig, TConfigProvider>(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TConfig, TConfigProvider>(configProvider, httpClientFactory)
    , IBaseWeChatService<TConfig>
    where TConfig : BaseWeChatConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    protected readonly IBaseRedisService _redisService = redisService;
    protected readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

    public async Task<string?> VerifyTokenAsync(HttpContext httpContext)
    {
        var config = await GetConfigAsync();
        var signature = httpContext.Request.Query["signature"].FirstOrDefault() ?? "";
        var nonce = httpContext.Request.Query["nonce"].FirstOrDefault() ?? "";
        var timestamp = httpContext.Request.Query["timestamp"].FirstOrDefault() ?? "";
        var echoStr = httpContext.Request.Query["echostr"].FirstOrDefault() ?? "";
        var token = config.Token;
        var paras = new string[] { timestamp, nonce, token };
        Array.Sort(paras);
        var dataToSign = string.Join("", paras);
        var hashBytes = SHA1.HashData(Encoding.UTF8.GetBytes(dataToSign));
        var hash = Utils.StringUtils.ConvertToHexString(hashBytes);
        return hash == signature ? echoStr : "";
    }

    public async Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"WeChat:{config.AppId}:AccessToken");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<AccessTokenOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetAccessTokenAsync(false);
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, TimeSpan.FromSeconds(res.ExpiresIn - 30));
            }
        }
        var url = $"cgi-bin/token?grant_type=client_credential&appid={config.AppId}&secret={config.AppSecret}";
        return await GetAsync<AccessTokenOutput>(url);
    }

    public async Task<AccessTokenOutput> GetStableAccessTokenAsync(bool enableCache = false)
    {
        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"WeChat:{config.AppId}:StableAccessToken");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<AccessTokenOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetStableAccessTokenAsync(false);
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, new TimeSpan(0, 0, res.ExpiresIn - 30));
                return res;
            }
        }
        var url = $"cgi-bin/stable_token";
        var input = new Dictionary<string, object>()
        {
            ["grant_type"] = "client_credential",
            ["appid"] = config.AppId,
            ["secret"] = config.AppSecret,
            //["force_refresh"] = true,
        };
        return await PostAsync<AccessTokenOutput>(url, input);
    }

    public async Task<TicketOutput> GetTicketAsync(string? type = null, bool enableCache = false)
    {
        type ??= "jsapi";

        var config = await GetConfigAsync();
        if (enableCache)
        {
            var rdb = _redisService.GetDatabase();
            var key = _redisKeyBuilder.Build($"WeChat:{config.AppId}:Ticket:{type}");
            var value = await rdb.StringGetAsync(key);
            if (value.HasValue)
            {
                var res = JsonUtil.Deserialize<TicketOutput>(value.ToString());
                return res ?? throw new Exception("反序列化缓存失败");
            }
            else
            {
                var res = await GetTicketAsync(type, false);
                value = JsonUtil.Serialize(res);
                await rdb.StringSetAsync(key, value, new TimeSpan(0, 0, res.ExpiresIn - 30));
                return res;
            }
        }
        var token = await GetStableAccessTokenAsync(true);
        var url = $"cgi-bin/ticket/getticket?access_token={token.AccessToken}&type={type}";
        return await GetAsync<TicketOutput>(url);
    }

    public async Task<SignOutput> SignAsync(SignInput input)
    {
        var config = await GetConfigAsync();
        var output = new SignOutput()
        {
            AppId = config.AppId,
            NonceStr = Utils.StringUtils.GenerateRandomString(16),
            Timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Signature = "",
        };
        var ticket = await GetTicketAsync(null, true);
        var sorted = new SortedDictionary<string, string?>()
        {
            { "noncestr", output.NonceStr },
            { "timestamp", output.Timestamp.ToString() },
            { "jsapi_ticket", ticket.Ticket },
            { "url", input.Url },
        };
        var dataToSign = Utils.StringUtils.ToQueryString(sorted, true);
        var hashBytes = SHA1.HashData(Encoding.UTF8.GetBytes(dataToSign));
        output.Signature = Utils.StringUtils.ConvertToHexString(hashBytes);
        return output;
    }

    public async Task<string> GenerateAuthUrlAsync(string redirectUrl, string? scope = null)
    {
        var config = await GetConfigAsync();
        return $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={config.AppId}&redirect_uri={Uri.EscapeDataString(redirectUrl)}&response_type=code&scope={scope ?? "snsapi_userinfo"}&state=bind";
    }

    public async Task<string> GenerateLoginQrCodeUrlAsync(string redirectUrl, string? scope = null)
    {
        var config = await GetConfigAsync();
        return $"https://open.weixin.qq.com/connect/qrconnect?appid={config.AppId}&redirect_uri={Uri.EscapeDataString(redirectUrl)}&response_type=code&scope={scope ?? "snsapi_login"}&state=login";
    }

    public async Task<AuthTokenOutput> GetAuthTokenByCodeAsync(string code)
    {
        var config = await GetConfigAsync();
        var url = $"sns/oauth2/access_token?appid={config.AppId}&secret={config.AppSecret}&code={code}&grant_type=authorization_code";
        return await GetAsync<AuthTokenOutput>(url);
    }

    public async Task<UserOutput> GetUserByAccessTokenAsync(string token, string openId)
    {
        var url = $"sns/userinfo?access_token={token}&openid={openId}&lang=zh_CN";
        return await GetAsync<UserOutput>(url);
    }
}
