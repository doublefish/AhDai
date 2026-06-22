using AhDai.Integration.AntChain.Configs;
using AhDai.Integration.AntChain.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AntChainNotaryConfigProvider(IOptionsMonitor<AntChainNotaryConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AntChainNotaryConfig>(options, parameterService)
    , IAntChainNotaryConfigProvider
{
}
