using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatMiniProgramConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatMiniProgramConfig>(configuration, parameterService)
    , IWeChatMiniProgramConfigProvider
{
    protected override long GetTenantId() => 0;
}
