using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class WeChatOfficialAccountConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<WeChatOfficialAccountConfig>(configuration, parameterService)
    , IWeChatOfficialAccountConfigProvider
{
    protected override long GetTenantId() => 0;
}
