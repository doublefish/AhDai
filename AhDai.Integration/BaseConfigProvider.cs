using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Integration;

/// <summary>
/// BaseConfigProvider
/// </summary>
/// <typeparam name="TConfig"></typeparam>
/// <param name="configuration"></param>
public abstract class BaseConfigProvider<TConfig>(IConfiguration configuration)
    : IBaseConfigProvider<TConfig>
    where TConfig : class, new()
{
    /// <summary>
    /// 系统配置
    /// </summary>
    protected readonly IConfiguration _configuration = configuration;
    /// <summary>
    /// 配置缓存
    /// </summary>
    protected readonly Dictionary<long, TConfig> _cachedConfigs = [];

    /// <summary>
    /// 获取租户Id
    /// </summary>
    /// <returns></returns>
    protected abstract long GetTenantId();

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <returns></returns>
    public virtual async Task<TConfig> GetAsync()
    {
        var tenantId = GetTenantId();
        if (_cachedConfigs.TryGetValue(tenantId, out var cachedConfig))
        {
            return cachedConfig;
        }

        var type = typeof(TConfig);
        var name = type.Name[..^6];
        var config = await GetAsync(name, tenantId);
        _cachedConfigs[tenantId] = config;
        return config;
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual Task<TConfig> GetAsync(string name)
    {
        var config = _configuration.GetSection(name).Get<TConfig>() ?? throw new Exception($"未读取到配置：{name}");
        return Task.FromResult(config);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    protected virtual Task<TConfig> GetAsync(string name, long tenantId) => GetAsync(name);
}
