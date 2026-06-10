using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;

namespace AhDai.Service.Providers.Integration;

[Attributes.Service]
internal class AliyunOcrConfigProvider(IConfiguration configuration, IParameterService parameterService)
    : BaseIntegrationConfigProvider<AliyunOcrConfig>(configuration, parameterService)
    , IAliyunOcrConfigProvider
{
}
