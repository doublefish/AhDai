using AhDai.Integration.Hikvision.Configs;
using AhDai.Integration.Hikvision.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class HikIoTConfigProvider(IOptionsMonitor<HikIoTConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<HikIoTConfig>(options, parameterService), IHikIoTConfigProvider
{
}
