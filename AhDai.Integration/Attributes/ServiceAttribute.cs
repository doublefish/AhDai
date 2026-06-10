using Microsoft.Extensions.DependencyInjection;
using System;

namespace AhDai.Integration.Attributes;

/// <summary>
/// ServiceAttribute
/// </summary>
/// <param name="lifetime"></param>
[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
    /// <summary>
    /// 生命周期
    /// </summary>
    public ServiceLifetime Lifetime { get; set; } = lifetime;
}
