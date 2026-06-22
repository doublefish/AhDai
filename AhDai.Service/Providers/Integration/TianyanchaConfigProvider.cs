using AhDai.Integration.Tianyancha.Configs;
using AhDai.Integration.Tianyancha.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class TianyanchaConfigProvider(IOptionsMonitor<TianyanchaConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<TianyanchaConfig>(options, parameterService)
    , ITianyanchaConfigProvider
{
}
