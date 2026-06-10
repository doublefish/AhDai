using AhDai.Integration.Tencent.Configs;
using AhDai.Integration.Tencent.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class TencentMapConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<TencentMapConfig>(configuration, parameterService)
    , ITencentMapConfigProvider
{
}
