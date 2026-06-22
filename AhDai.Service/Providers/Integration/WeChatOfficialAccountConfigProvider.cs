using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatOfficialAccountConfigProvider(IOptionsMonitor<WeChatOfficialAccountConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatOfficialAccountConfig>(options, parameterService)
    , IWeChatOfficialAccountConfigProvider
{
    protected override long GetTenantId() => 0;
}
