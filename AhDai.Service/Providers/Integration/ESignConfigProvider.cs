using AhDai.Integration.ESign.Configs;
using AhDai.Integration.ESign.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class ESignConfigProvider(IOptionsMonitor<ESignConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<ESignConfig>(options, parameterService)
    , IESignConfigProvider
{
}
