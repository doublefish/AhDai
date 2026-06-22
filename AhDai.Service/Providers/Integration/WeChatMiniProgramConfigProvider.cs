using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatMiniProgramConfigProvider(IOptionsMonitor<WeChatMiniProgramConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatMiniProgramConfig>(options, parameterService)
    , IWeChatMiniProgramConfigProvider
{
    protected override long GetTenantId() => 0;
}
