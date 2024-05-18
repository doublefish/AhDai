using System;
using System.Threading.Tasks;

namespace AhDai.Service.Base;

/// <summary>
/// 订阅者服务
/// </summary>
public interface ISubscriberService
{
	/// <summary>
	/// 订阅
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="channel"></param>
	/// <param name="handler"></param>
	/// <returns></returns>
	Task SubscribeAsync<T>(string channel, Action<string, T> handler);
}
