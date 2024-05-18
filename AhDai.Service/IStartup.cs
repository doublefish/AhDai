using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
	/// <returns></returns>
	void ConfigureServices(IServiceCollection services, IConfiguration configuration);

}
