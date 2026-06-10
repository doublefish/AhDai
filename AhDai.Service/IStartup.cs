using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AhDai.Service;

/// <summary>
/// IStartup
/// </summary>
public interface IStartup
{
    /// <summary>
    /// ConfigureServices
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="isWorker"></param>
    /// <returns></returns>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration, bool isWorker = false);

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="app"></param>
    void Configure(WebApplication app);

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="app"></param>
    void Configure(IHost app);
}
