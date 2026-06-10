using AhDai.Integration.ESign.Configs;
using AhDai.Integration.ESign.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class ESignConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<ESignConfig>(configuration, parameterService)
    , IESignConfigProvider
{
}
