using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class BaiduFaceprintConfigProvider(IOptionsMonitor<BaiduFaceprintConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<BaiduFaceprintConfig>(options, parameterService), IBaiduFaceprintConfigProvider
{
}
