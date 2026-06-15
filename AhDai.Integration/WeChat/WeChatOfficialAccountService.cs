using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models.OfficialAccount;
using AhDai.Integration.WeChat.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatOfficialAccountService
/// </summary>
[Attributes.Service()]
internal class WeChatOfficialAccountService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IWeChatOfficialAccountConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseWeChatService<WeChatOfficialAccountConfig, IWeChatOfficialAccountConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IWeChatOfficialAccountService
{
    protected override string ServiceName => "微信公众号";


    public async Task<QrCodeCreateOutput> CreateQrCodeAsync(string token, QrCodeCreateInput input)
    {
        var url = $"cgi-bin/qrcode/create?access_token={token}";
        return await PostAsync<QrCodeCreateOutput>(url, input);
    }

    public async Task<TemplateMessageOutput> SendTemplateMessageAsync(string token, TemplateMessageInput input)
    {
        var config = await GetConfigAsync();
        // 避免30秒内连续发送
        var rdb = _redisService.GetDatabase();
        var key = _redisKeyBuilder.Build($"WeChat:{config.AppId}:TemplateId:{input.TemplateId}:ToUser:{input.ToUser}");
        while (true)
        {
            var ok = await rdb.StringSetAsync(key, DateTime.Now.ToString("HH:mm:ss.fff"), TimeSpan.FromSeconds(30), StackExchange.Redis.When.NotExists);
            if (ok) break;
            if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("避免连续发送造成发送失败，等待发送");
            await Task.Delay(3000);
        }
        var url = $"cgi-bin/message/template/send?access_token={token}";
        try
        {
            var res = await PostAsync<TemplateMessageOutput>(url, input);
            await rdb.StringSetAsync(key, DateTime.Now.ToString("HH:mm:ss.fff"), TimeSpan.FromSeconds(30), StackExchange.Redis.When.Always);
            return res;
        }
        catch
        {
            await rdb.KeyDeleteAsync(key);
            throw;
        }
    }

    public async Task<SubscribeMessageOutput> SendSubscribeMessageAsync(string token, SubscribeMessageInput input)
    {
        var url = $"cgi-bin/message/subscribe/bizsend?access_token={token}";
        return await PostAsync<SubscribeMessageOutput>(url, input);
    }
}
