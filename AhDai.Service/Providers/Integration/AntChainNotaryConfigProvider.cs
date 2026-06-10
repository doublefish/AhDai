using AhDai.Integration.AntChain.Configs;
using AhDai.Integration.AntChain.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AntChainNotaryConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AntChainNotaryConfig>(configuration, parameterService)
    , IAntChainNotaryConfigProvider
{
}
