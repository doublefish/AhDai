using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace AhDai.Core.Log4net;

/// <summary>
/// Log4netUtil
/// </summary>
public static class Log4netUtil
{
    static ILoggerRepository? repository;
    static readonly ILog Log = LogManager.GetLogger(Repository.Name, "Log");

    /// <summary>
    /// Repository
    /// </summary>
    public static ILoggerRepository Repository
    {
        get
        {
            if (repository == null)
            {
                repository = LogManager.CreateRepository("Log4net");
                XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            }
            return repository;
        }
    }

    /// <summary>
    /// Info
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Info(string message, Exception? exception = null)
    {
        Log.Info(message, exception);
    }

    /// <summary>
    /// Info
    /// </summary>
    /// <param name="eventId">事件Id</param>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Info(string eventId, string message, Exception? exception = null)
    {
        Log.Info($"[{eventId}]{message}", exception);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Error(string message, Exception? exception = null)
    {
        Log.Error(message, exception);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="eventId">事件Id</param>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Error(string eventId, string message, Exception? exception = null)
    {
        Log.Error($"[{eventId}]{message}", exception);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Debug(string message, Exception? exception = null)
    {
        Log.Debug(message, exception);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="eventId">事件Id</param>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Debug(string eventId, string message, Exception? exception = null)
    {
        Log.Debug($"[{eventId}]{message}", exception);
    }
}
