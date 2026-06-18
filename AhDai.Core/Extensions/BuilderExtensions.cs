using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AhDai.Core.Extensions;

/// <summary>
/// BuilderExtensions
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// ConfigureFile
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureFile(this IHostApplicationBuilder builder, string key = "File")
    {
        return builder.Configure<Options.FileOptions>(key);
    }

    /// <summary>
    /// ConfigureJwt
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureJwt(this IHostApplicationBuilder builder, string key = "Jwt")
    {
        return builder.Configure<Options.JwtOptions>(key);
    }

    /// <summary>
    /// ConfigureMail
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureMail(this IHostApplicationBuilder builder, string key = "Mail")
    {
        return builder.Configure<Options.MailOptions>(key);
    }

    /// <summary>
    /// ConfigureRedis
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureRedis(this IHostApplicationBuilder builder, string key = "Redis")
    {
        return builder.Configure<Options.RedisOptions>(key);
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder Configure<TOptions>(this IHostApplicationBuilder builder, string key)
        where TOptions : class
    {
        builder.Services.Configure<TOptions>(builder.Configuration.GetSection(key));
        //builder.Services.AddOptions<TOptions>().Bind(builder.Configuration.GetSection(key)).PostConfigure(o =>
        //{
        //}).ValidateDataAnnotations().ValidateOnStart();
        return builder;
    }
}
