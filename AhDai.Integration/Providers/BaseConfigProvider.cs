using AhDai.Integration.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AhDai.Integration.Providers;

/// <summary>
/// BaseConfigProvider
/// </summary>
/// <typeparam name="TConfig"></typeparam>
/// <param name="options"></param>
public abstract class BaseConfigProvider<TConfig>(IOptionsMonitor<TConfig> options)
    : IBaseConfigProvider<TConfig>
    where TConfig : class, new()
{
    /// <summary>
    /// 系统配置
    /// </summary>
    protected readonly IOptionsMonitor<TConfig> _options = options;

    /// <summary>
    /// 获取租户Id
    /// </summary>
    /// <returns></returns>
    protected abstract long GetTenantId();

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <returns></returns>
    public virtual async ValueTask<TConfig> GetAsync()
    {
        var tenantId = GetTenantId();
        return await GetAsync(tenantId);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    protected virtual ValueTask<TConfig> GetAsync(long tenantId)
    {
        var config = _options.CurrentValue ?? throw new Exception("未读取到配置");
        return ValueTask.FromResult(config);
    }
}
