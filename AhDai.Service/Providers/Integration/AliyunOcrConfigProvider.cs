using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Options;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AliyunOcrConfigProvider(IOptionsMonitor<AliyunOcrConfig> options, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AliyunOcrConfig>(options, parameterService)
    , IAliyunOcrConfigProvider
{
}
