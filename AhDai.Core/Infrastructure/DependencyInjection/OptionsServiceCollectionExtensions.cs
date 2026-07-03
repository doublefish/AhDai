using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.DependencyInjection;

/// <summary>
/// OptionsServiceCollectionExtensions
/// </summary>
public static class OptionsServiceCollectionExtensions
{
    /// <summary>
    /// AddOptions
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string key)
        where TOptions : class
    {
        services.Configure<TOptions>(configuration.GetSection(key));
        //services.AddOptions<TOptions>().Bind(configuration.GetSection(key)).PostConfigure(o =>
        //{
        //}).ValidateDataAnnotations().ValidateOnStart();
        return services;
    }
}
