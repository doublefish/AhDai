using AhDai.Integration.Tencent.Configs;
using AhDai.Integration.Tencent.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class TencentMapConfigProvider(IOptionsMonitor<TencentMapConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<TencentMapConfig>(options, parameterService)
    , ITencentMapConfigProvider
{
}
