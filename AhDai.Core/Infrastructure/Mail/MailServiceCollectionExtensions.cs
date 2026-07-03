using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.Mail;

/// <summary>
/// MailServiceCollectionExtensions
/// </summary>
public static class MailServiceCollectionExtensions
{
    /// <summary>
    /// AddMailService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration, string key = "Mail")
    {
        services.AddOptions<MailOptions>(configuration, key);
        services.AddSingleton<IBaseMailService, BaseMailService>();
        return services;
    }
}
