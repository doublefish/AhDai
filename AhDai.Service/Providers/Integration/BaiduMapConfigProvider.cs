using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class BaiduMapConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<BaiduMapConfig>(configuration, parameterService), IBaiduMapConfigProvider
{
}
