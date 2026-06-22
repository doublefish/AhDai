using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class BaiduOcrConfigProvider(IOptionsMonitor<BaiduOcrConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<BaiduOcrConfig>(options, parameterService), IBaiduOcrConfigProvider
{
}
