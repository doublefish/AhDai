using AhDai.Integration.Amap.Configs;
using AhDai.Integration.Amap.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AmapConfigProvider(IOptionsMonitor<AmapConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AmapConfig>(options, parameterService)
    , IAmapConfigProvider
{
}
