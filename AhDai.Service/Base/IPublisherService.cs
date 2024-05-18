using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.Base;

/// <summary>
/// 发布者服务
/// </summary>
public interface IPublisherService
{
	/// <summary>
	/// 发布
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="channel"></param>
	/// <param name="body"></param>
	/// <returns></returns>
	Task PublishAsync<T>(string channel, T body);

	/// <summary>
	/// 发布
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="channel"></param>
	/// <param name="headers"></param>
	/// <param name="body"></param>
	/// <returns></returns>
	Task PublishAsync<T>(string channel, IDictionary<string, string> headers, T body);
}
