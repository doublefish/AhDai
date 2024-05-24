using AhDai.Core.Extensions;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;


namespace AhDai.Core.Services;

/// <summary>
/// Log4NetLogger
/// </summary>
public class Log4NetLogger : Microsoft.Extensions.Logging.ILogger
{
    private readonly IExternalScopeProvider externalScopeProvider;

    private readonly log4net.Core.ILogger logger;

    /// <summary>
    /// Name
    /// </summary>
    public string Name => logger.Name;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="name"></param>
    /// <param name="externalScopeProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Log4NetLogger(string repository, string name, IExternalScopeProvider externalScopeProvider)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        this.externalScopeProvider = externalScopeProvider ?? throw new ArgumentNullException(nameof(externalScopeProvider));
        logger = LogManager.GetLogger(repository, name).Logger;
    }

    /// <summary>
    /// BeginScope
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="state"></param>
    /// <returns></returns>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return externalScopeProvider.Push(state);
    }

    /// <summary>
    /// IsEnabled
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        var level = logLevel.ToLog4netLevel();
        if (level != null)
        {
            return logger.IsEnabledFor(level);
        }
        if (logLevel == LogLevel.None)
        {
            return false;
        }
        throw new ArgumentOutOfRangeException(nameof(logLevel));
    }

    /// <summary>
    /// Log
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="state"></param>
    /// <param name="exception"></param>
    /// <param name="formatter"></param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            ArgumentNullException.ThrowIfNull(formatter);

            var typeFromHandle = typeof(LoggerExtensions);
            var text = formatter(state, exception);
            var level = logLevel.ToLog4netLevel();
            var loggingEvent = new LoggingEvent(typeFromHandle, logger.Repository, logger.Name, level, text, exception);
            if (loggingEvent != null)
            {
                logger.Log(loggingEvent);
            }
        }
    }
}
