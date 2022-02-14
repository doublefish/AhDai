using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace AhDai.Core.Interfaces
{
	/// <summary>
	/// 订阅服务
	/// </summary>
	public interface ISubscribeService : IRabbitMQService
	{
		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="received">接收消息处理方法</param>
		/// <returns></returns>
		public string Subscribe(string queue, Func<object, BasicDeliverEventArgs, RabbitMQ.ResultType> received);
	}
}
