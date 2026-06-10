using AhDai.Integration.Amap.Configs;
using AhDai.Integration.Amap.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AmapConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AmapConfig>(configuration, parameterService)
    , IAmapConfigProvider
{
}
