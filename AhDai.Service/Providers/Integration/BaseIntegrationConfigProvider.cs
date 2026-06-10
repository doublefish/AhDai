using AhDai.Core.Extensions;
using AhDai.Core.Metadata;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Providers;
using AhDai.Service.System.Parameter;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AhDai.Service.Providers.Integration;

internal abstract class BaseIntegrationConfigProvider<TConfig>(IConfiguration configuration, IParameterService parameterService)
    : BaseConfigProvider<TConfig>(configuration), IBaseConfigProvider<TConfig>
    where TConfig : class, new()
{
    protected readonly IParameterService _parameterService = parameterService;

    static readonly PropertyInfo[] _cachedProperties = [.. TypeMetadataProvider.GetProperties<TConfig>().Select(x => x.Info)];

    protected override long GetTenantId() => 10000;

    public override Task<TConfig> GetAsync() => base.GetAsync();

    protected override Task<TConfig> GetAsync(string name) => base.GetAsync(name);

    protected override async Task<TConfig> GetAsync(string name, long tenantId)
    {
        if (tenantId == 0)
        {
            return await GetAsync(name);
        }

        var paraValues = await _parameterService.GetValueByCategoryAsync(name, tenantId);
        var hasParaValue = false;
        var config = new TConfig();
        foreach (var p in _cachedProperties)
        {
            if (!p.CanWrite) continue;

            if (paraValues.TryGetValue(p.Name, out var stringValue) && stringValue != null)
            {
                hasParaValue = true;
                try
                {
                    p.SetValueExt(config, stringValue);
                }
                catch (Exception ex)
                {
                    throw new Exception($"配置项 [{name}.{p.Name}] 类型转换失败：{ex.Message}", ex);
                }
            }
        }
        if (!hasParaValue) throw new Exception($"未读取到配置（系统参数）：{name}");

        return config;
    }
}
