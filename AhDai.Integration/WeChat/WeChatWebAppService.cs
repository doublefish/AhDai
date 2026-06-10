using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using System.Net.Http;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatWebAppService
/// </summary>
[Attributes.Service()]
internal class WeChatWebAppService(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, IWeChatWebAppConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseWeChatService<WeChatWebAppConfig, IWeChatWebAppConfigProvider>(redisService, redisKeyBuilder, configProvider, httpClientFactory)
    , IWeChatWebAppService
{
    protected override string ServiceName => "微信网站应用";
}
