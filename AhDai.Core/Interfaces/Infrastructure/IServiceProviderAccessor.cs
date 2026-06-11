using System;

namespace AhDai.Core.Interfaces.Infrastructure;

/// <summary>
/// 作用域服务持有者，解决静态工具类与 DI 容器的桥接
/// </summary>
public interface IServiceProviderAccessor
{
    /// <summary>
    /// Current
    /// </summary>
    IServiceProvider? Current { get; set; }
}
