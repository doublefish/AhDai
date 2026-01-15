using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AhDai.Core.Utils;

/// <summary>
/// LoggerUtil
/// </summary>
public static class LoggerUtil
{
    /// <summary>
    /// 构造函数
    /// </summary>
    static LoggerUtil()
    {

    }

    /// <summary>
    /// GetLogger
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="creationMode">创建模式 (0: Factory, 1: Provider)</param>
    /// <returns></returns>
    public static ILogger GetLogger<T>(int creationMode = 0) => GetLogger(typeof(T), creationMode);

    /// <summary>
    /// GetLogger
    /// </summary>
    /// <param name="type"></param>
    /// <param name="creationMode">创建模式 (0: Factory, 1: Provider)</param>
    /// <returns></returns>
    public static ILogger GetLogger(Type type, int creationMode = 0) => GetLogger(type.FullName ?? type.Name, creationMode);

    /// <summary>
    /// GetLogger
    /// </summary>
    /// <param name="categoryName">日志类别名称</param>
    /// <param name="creationMode">创建模式 (0: Factory, 1: Provider)</param>
    /// <returns></returns>
    public static ILogger GetLogger(string categoryName, int creationMode = 0)
    {
        if (ServiceUtil.Services == null)
        {
            throw new Exception("未初始化ServiceHelper.Services");
        }
        if (creationMode == 1)
        {
            var provider = ServiceUtil.Services.GetRequiredService<ILoggerProvider>();
            return provider.CreateLogger(categoryName);
        }
        else
        {
            // 复用同名对象
            var factory = ServiceUtil.Services.GetRequiredService<ILoggerFactory>();
            return factory.CreateLogger(categoryName);
        }
    }
}
