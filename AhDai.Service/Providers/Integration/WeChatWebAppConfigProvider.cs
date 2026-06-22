using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatWebAppConfigProvider(IOptionsMonitor<WeChatWebAppConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatWebAppConfig>(options, parameterService)
    , IWeChatWebAppConfigProvider
{
    protected override long GetTenantId() => 0;
}
