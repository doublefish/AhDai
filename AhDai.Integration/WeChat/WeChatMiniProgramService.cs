using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models.MiniProgram;
using AhDai.Integration.WeChat.Providers;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatMiniProgramService
/// </summary>
[Attributes.Service()]
internal class WeChatMiniProgramService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IWeChatMiniProgramConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseWeChatService<WeChatMiniProgramConfig, IWeChatMiniProgramConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IWeChatMiniProgramService
{
    protected override string ServiceName => "微信小程序";


    public async Task<SessionOutput> GetSessionByJsCodeAsync(string code)
    {
        var config = await GetConfigAsync();
        var url = $"sns/jscode2session?appid={config.AppId}&secret={config.AppSecret}&js_code={code}&grant_type=authorization_code";
        return await GetAsync<SessionOutput>(url);
    }
}
