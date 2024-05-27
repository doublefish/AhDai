using Microsoft.Extensions.Logging;

namespace AhDai.Core.Extensions;

/// <summary>
/// LoggingExtensions
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// GetName
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public static string GetName(this LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "Trace",
            LogLevel.Debug => "Debug",
            LogLevel.Information => "Info",
            LogLevel.Warning => "Warn",
            LogLevel.Error => "Error",
            LogLevel.Critical => "Critical",
            _ => string.Empty
        };
    }
}
