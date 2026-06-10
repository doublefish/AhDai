using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatWebAppConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatWebAppConfig>(configuration, parameterService)
    , IWeChatWebAppConfigProvider
{
    protected override long GetTenantId() => 0;
}
