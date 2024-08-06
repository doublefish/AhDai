using Microsoft.Extensions.Configuration;
using System;

namespace AhDai.Core.Utils;

/// <summary>
/// ServiceHelper
/// </summary>
public static class ServiceUtil
{
    /// <summary>
    /// 服务实例
    /// </summary>
    public static IServiceProvider Services { get; private set; } = default!;
    /// <summary>
    /// Configuration
    /// </summary>
    public static IConfiguration Configuration { get; private set; } = default!;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void Init(IServiceProvider services, IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }
}
