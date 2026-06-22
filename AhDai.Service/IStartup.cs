using Microsoft.AspNetCore.Builder;
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
    /// <param name="builder"></param>
    /// <param name="isWorker"></param>
    /// <returns></returns>
    void ConfigureServices(IHostApplicationBuilder builder, bool isWorker = false);

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
