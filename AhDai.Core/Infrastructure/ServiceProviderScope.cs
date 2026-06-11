using AhDai.Core.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AhDai.Core.Infrastructure;

/// <summary>
/// 一个可释放的上下文开关
/// </summary>
public readonly struct ServiceProviderScope : IDisposable
{
    private readonly IServiceProviderAccessor _accessor;
    private readonly IServiceProvider? _original;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    public ServiceProviderScope(IServiceProvider provider)
    {
        _accessor = provider.GetRequiredService<IServiceProviderAccessor>();
        _original = _accessor.Current;
        _accessor.Current = provider;
    }

    /// <summary>
    /// 释放
    /// </summary>
    public readonly void Dispose()
    {
        _accessor.Current = _original;
    }
}
