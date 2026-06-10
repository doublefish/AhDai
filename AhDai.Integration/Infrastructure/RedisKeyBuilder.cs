using AhDai.Integration.Abstractions;
using AhDai.Integration.Options;
using Microsoft.Extensions.Options;

namespace AhDai.Integration.Infrastructure;

/// <summary>
/// RedisKeyBuilder
/// </summary>
/// <param name="options"></param>
internal class RedisKeyBuilder(IOptions<IntegrationOptions> options) : IRedisKeyBuilder
{
    readonly IntegrationOptions _options = options.Value;

    /// <summary>
    /// Build
    /// </summary>
    /// <param name="segments"></param>
    /// <returns></returns>
    public string Build(params string[] segments)
    {
        return $"{_options.RedisKeyPrefix}:{string.Join(':', segments)}";
    }
}
