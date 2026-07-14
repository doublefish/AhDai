using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace AhDai.Core.Utils;

/// <summary>
/// LoggerUtil
/// </summary>
public static class LoggerUtil
{
    static ILoggerFactory? _factory;

    /// <summary>
    /// 允许在系统启动时手动注册或通过 DI 容器自动解耦注入工厂。
    /// </summary>
    /// <param name="factory"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Initialize(ILoggerFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// 获取日志器
    /// </summary>
    /// <typeparam name="T">业务分类类型</typeparam>
    public static ILogger<T> GetLogger<T>()
    {
        return ResolveFactory().CreateLogger<T>();
    }

    /// <summary>
    /// 获取日志器
    /// </summary>
    public static ILogger GetLogger(Type type)
    {
        return ResolveFactory().CreateLogger(type);
    }

    /// <summary>
    /// 获取日志器
    /// </summary>
    /// <param name="categoryName">日志类别名称（如 "AhDai.Core.DigitalChain"）</param>
    public static ILogger GetLogger(string categoryName)
    {
        return ResolveFactory().CreateLogger(categoryName);
    }

    /// <summary>
    /// 动态解析或兜底日志工厂
    /// </summary>
    static ILoggerFactory ResolveFactory()
    {
        if (_factory != null) return _factory;

        if (ServiceUtil.Services != null)
        {
            var factory = ServiceUtil.Services.GetService<ILoggerFactory>();
            if (factory != null)
            {
                _factory = factory;
                return _factory;
            }
        }

        return NullLoggerFactory.Instance;
    }
}
