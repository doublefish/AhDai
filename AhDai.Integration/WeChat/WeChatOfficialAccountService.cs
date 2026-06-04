using AhDai.Core.Interfaces.Services;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatOfficialAccountService
/// </summary>
internal class WeChatOfficialAccountService(IBaseRedisService redisService, IWeChatOfficialAccountConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : WeChatService<WeChatOfficialAccountConfig, IWeChatOfficialAccountConfigProvider>(redisService, configProvider, httpClientFactory), IWeChatOfficialAccountService
{
    public async Task<QrCodeCreateOutput> CreateQrCodeAsync(string token, QrCodeCreateInput input)
    {
        var config = await GetConfigAsync();
        var url = $"cgi-bin/qrcode/create?access_token={token}";
        return await SendAsync<QrCodeCreateOutput, QrCodeCreateInput>(config, HttpMethod.Post, url, input);
    }

    public async Task<TemplateMessageOutput> SendTemplateMessageAsync(string token, TemplateMessageInput input)
    {
        var config = await GetConfigAsync();
        // 避免30秒内连续发送
        var rdb = _redisService.GetDatabase();
        var key = $"AhDai:WeChat:{config.AppId}:TemplateId:{input.TemplateId}:ToUser:{input.ToUser}";
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
            var res = await SendAsync<TemplateMessageOutput, TemplateMessageInput>(config, HttpMethod.Post, url, input);
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
        var config = await GetConfigAsync();
        var url = $"cgi-bin/message/subscribe/bizsend?access_token={token}";
        return await SendAsync<SubscribeMessageOutput, SubscribeMessageInput>(config, HttpMethod.Post, url, input);
    }
}
