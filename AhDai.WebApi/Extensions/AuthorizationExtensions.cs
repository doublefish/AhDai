using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.WebApi.Extensions;

/// <summary>
/// AuthorizationExtensions
/// </summary>
public static class AuthorizationExtensions
{
	/// <summary>
	/// AddMyAuthorization
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection AddMyAuthorization(this IServiceCollection services, IConfiguration configuration)
	{
		return services;
	}
}
