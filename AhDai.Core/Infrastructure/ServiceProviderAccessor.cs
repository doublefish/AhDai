using AhDai.Core.Interfaces.Infrastructure;
using System;
using System.Threading;

namespace AhDai.Core.Infrastructure;

/// <summary>
///  作用域服务持有者，解决静态工具类与 DI 容器的桥接
/// </summary>
public class ServiceProviderAccessor : IServiceProviderAccessor
{
    readonly AsyncLocal<IServiceProvider?> _current = new();

    /// <summary>
    /// Current
    /// </summary>
    public IServiceProvider? Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}
