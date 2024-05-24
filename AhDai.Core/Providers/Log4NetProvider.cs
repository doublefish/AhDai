using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace AhDai.Core.Providers;

/// <summary>
/// Log4NetProvider
/// </summary>
public class Log4NetProvider : ILoggerProvider, IDisposable, ISupportExternalScope
{
    readonly ConcurrentDictionary<string, Services.Log4NetLogger> loggers = new();
    readonly ILoggerRepository loggerRepository;

    /// <summary>
    /// ExternalScopeProvider
    /// </summary>
    public IExternalScopeProvider ExternalScopeProvider { get; private set; } = default!;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configFile"></param>
    /// <param name="repository"></param>
    public Log4NetProvider(string configFile = "log4net.config", string repository = "log4net")
    {
        loggerRepository = LogManager.CreateRepository(repository);
        XmlConfigurator.Configure(loggerRepository, new FileInfo(configFile));
    }

    /// <summary>
    /// CreateLogger
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, (name) =>
        {
            return new Services.Log4NetLogger(loggerRepository.Name, name, ExternalScopeProvider);
        });
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        loggerRepository.Shutdown();
        loggers.Clear();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// SetScopeProvider
    /// </summary>
    /// <param name="scopeProvider"></param>
    public void SetScopeProvider(IExternalScopeProvider scopeProvider)
    {
        ExternalScopeProvider = scopeProvider;
    }
}
