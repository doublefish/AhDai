using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.Services
{
	/// <summary>
	/// RabbitMQ订阅服务
	/// </summary>
	public class SubscribeService : RabbitMQService, Interfaces.ISubscribeService
	{
		/// <summary>
		/// 通道
		/// </summary>
		public IModel Channel { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public SubscribeService(IConfiguration configuration) : base(configuration)
		{
			var connection = GetConnection();
			Channel = connection.CreateModel();
		}

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="received">接收消息处理方法</param>
		/// <returns></returns>
		public string Subscribe(string queue, Func<object, BasicDeliverEventArgs, RabbitMQ.ResultType> received)
		{
			return RabbitMQ.Extensions.BasicConsume(Channel, queue, received);
		}
	}
}
