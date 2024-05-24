using Microsoft.Extensions.Logging;
using System;

namespace AhDai.Core.Providers;

/// <summary>
/// LoggerProvider
/// </summary>
public class LoggerProvider : ILoggerProvider
{
    readonly Action<LogLevel, string, Exception?>? OnExecuted;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onExecuted"></param>
    public LoggerProvider(Action<LogLevel, string, Exception?>? onExecuted = null)
    {
        OnExecuted = onExecuted;
    }

    /// <summary>
    /// CreateLogger
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        var logger = new Services.Logger(OnExecuted);
        //Console.WriteLine($"CreateLogger=>{logger.EventId}");
        return logger;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
