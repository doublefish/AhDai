using AhDai.Core.Interfaces.Services;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatMiniProgramService
/// </summary>
internal class WeChatMiniProgramService(IBaseRedisService redisService, IWeChatMiniProgramConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : WeChatService<WeChatMiniProgramConfig, IWeChatMiniProgramConfigProvider>(redisService, configProvider, httpClientFactory), IWeChatMiniProgramService
{

    public async Task<SessionOutput> GetSessionByJsCodeAsync(string code)
    {
        var config = await GetConfigAsync();
        var url = $"sns/jscode2session?appid={config.AppId}&secret={config.AppSecret}&js_code={code}&grant_type=authorization_code";
        return await SendAsync<SessionOutput>(config, HttpMethod.Get, url);
    }
}
