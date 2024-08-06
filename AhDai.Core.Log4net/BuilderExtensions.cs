using Microsoft.Extensions.Logging;

namespace AhDai.Core.Log4net;

/// <summary>
/// BuilderExtensions
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// AddLog4Net
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configFile"></param>
    /// <param name="repository"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string configFile = "log4net.config", string repository = "log4net")
    {
        return builder.AddProvider(new Log4netProvider(configFile, repository));
    }
}
