using Microsoft.Extensions.DependencyInjection;
using System;

namespace AhDai.Service.Impl.Attributes;

/// <summary>
/// ServiceAttribute
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="lifetime">生命周期</param>
[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
	/// <summary>
	/// 生命周期
	/// </summary>
	public ServiceLifetime Lifetime { get; set; } = lifetime;
}
