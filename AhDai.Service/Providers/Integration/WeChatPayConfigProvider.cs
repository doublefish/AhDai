using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatPayConfigProvider(IOptionsMonitor<WeChatPayConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatPayConfig>(options, parameterService)
    , IWeChatPayConfigProvider
{
}
