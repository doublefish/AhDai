using AhDai.Integration.Hikvision.Configs;
using AhDai.Integration.Hikvision.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class HikIoTConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<HikIoTConfig>(configuration, parameterService), IHikIoTConfigProvider
{
}
