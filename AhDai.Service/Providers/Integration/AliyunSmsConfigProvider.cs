using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AliyunSmsConfigProvider(IOptionsMonitor<AliyunSmsConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AliyunSmsConfig>(options, parameterService)
    , IAliyunSmsConfigProvider
{
}
