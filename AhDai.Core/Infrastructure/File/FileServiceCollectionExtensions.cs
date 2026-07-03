using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.File;

/// <summary>
/// FileServiceCollectionExtensions
/// </summary>
public static class FileServiceCollectionExtensions
{
    /// <summary>
    /// AddFileService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration, string key = "File")
    {
        services.AddOptions<FileOptions>(configuration, key);
        services.AddSingleton<IBaseFileService, BaseFileService>();
        return services;
    }
}
