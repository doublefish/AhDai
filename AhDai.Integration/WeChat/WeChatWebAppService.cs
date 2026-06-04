using AhDai.Core.Interfaces.Services;
using AhDai.Integration.WeChat.Configs;
using System.Net.Http;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatWebAppService
/// </summary>
internal class WeChatWebAppService(IBaseRedisService redisService, IWeChatWebAppConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : WeChatService<WeChatWebAppConfig, IWeChatWebAppConfigProvider>(redisService, configProvider, httpClientFactory), IWeChatWebAppService
{
}
