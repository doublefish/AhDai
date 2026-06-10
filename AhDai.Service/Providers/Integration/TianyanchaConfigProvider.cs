using AhDai.Integration.Tianyancha.Configs;
using AhDai.Integration.Tianyancha.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class TianyanchaConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<TianyanchaConfig>(configuration, parameterService)
    , ITianyanchaConfigProvider
{
}
