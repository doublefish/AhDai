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
    /// <returns></returns>
    public static ILogger GetLogger<T>()
    {
        var type = typeof(T);
        return GetLogger(type.FullName ?? type.Name);
    }

    /// <summary>
    /// GetLogger
    /// </summary>
    /// <param name="categoryName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static ILogger GetLogger(string categoryName, int type = 0)
    {
        if (ServiceUtil.Services == null)
        {
            throw new Exception("未初始化ServiceHelper.Services");
        }
        if (type == 1)
        {
            var provider = ServiceUtil.Services.GetRequiredService<ILoggerProvider>();
            return provider.CreateLogger(categoryName);
        }
        else
        {
            var factory = ServiceUtil.Services.GetRequiredService<ILoggerFactory>();
            // 复用同名对象
            return factory.CreateLogger(categoryName);
        }
    }
}
