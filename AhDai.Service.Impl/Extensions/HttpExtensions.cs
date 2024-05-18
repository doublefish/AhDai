using System.Net.Http;

namespace AhDai.Service.Impl;

/// <summary>
/// HttpExtensions
/// </summary>
internal static class HttpExtensions
{
	/// <summary>
	/// CreateClient
	/// </summary>
	/// <param name="factory"></param>
	/// <param name="closeCertificateValidation"></param>
	/// <returns></returns>
	public static HttpClient CreateClient(this IHttpClientFactory factory, bool closeCertificateValidation)
	{
		if (closeCertificateValidation)
		{
			return factory.CreateClient("NoCertificateValidation");
		}
		return factory.CreateClient();
	}
}
