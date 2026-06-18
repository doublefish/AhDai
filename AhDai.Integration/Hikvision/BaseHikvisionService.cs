using AhDai.Core.Interfaces.Services;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using System.Net.Http;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// BaseHikvisionService
/// </summary>
internal abstract class BaseHikvisionService<TConfig, TConfigProvider>(IBaseRedisService redisService, IRedisKeyBuilder redisKeyBuilder, TConfigProvider configProvider, IHttpClientFactory httpClientFactory)
    : BaseService<TConfig, TConfigProvider>(configProvider, httpClientFactory)
    , IBaseHikvisionService<TConfig>
    where TConfig : Configs.BaseHikvisionConfig
    where TConfigProvider : IBaseConfigProvider<TConfig>
{
    protected readonly IBaseRedisService _redisService = redisService;
    protected readonly IRedisKeyBuilder _redisKeyBuilder = redisKeyBuilder;

}
