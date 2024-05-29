using Microsoft.Extensions.Configuration;
using System;

namespace AhDai.Core.Extensions;

/// <summary>
/// ConfigurationExtensions
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// 获取Jwt配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Configs.JwtConfig GetJwtConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Jwt").Get<Configs.JwtConfig>();
        ArgumentNullException.ThrowIfNull(config);
        return config;
    }

    /// <summary>
    /// 获取邮件配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Configs.MailConfig GetMailConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Mail").Get<Configs.MailConfig>();
        ArgumentNullException.ThrowIfNull(config);
        return config;
    }

    /// <summary>
    /// 获取Redis配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Configs.RedisConfig GetRedisConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Redis").Get<Configs.RedisConfig>();
        ArgumentNullException.ThrowIfNull(config);
        return config;
    }

    /// <summary>
    /// 获取File配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Configs.FileConfig GetFileConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("File").Get<Configs.FileConfig>();
        ArgumentNullException.ThrowIfNull(config);
        return config;
    }
}
