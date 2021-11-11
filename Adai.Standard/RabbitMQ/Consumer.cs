using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 消费者
	/// </summary>
	public class Consumer : Base
	{
		/// <summary>
		/// 业务方法
		/// </summary>
		protected Action<object, BasicDeliverEventArgs> Action { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		public Consumer(ILogger logger) : this(logger, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="action"></param>
		public Consumer(ILogger logger, Action<object, BasicDeliverEventArgs> action) : base(logger)
		{
			Action = action;
		}

		/// <summary>
		/// 订阅消息
		/// </summary>
		/// <returns></returns>
		public string Subscribe()
		{
			Init();

			// 消费者
			var consumer = new EventingBasicConsumer(Channel);
			// 绑定
			Channel.QueueBind(Queue, Exchange, RoutingKey);
			consumer.Received += Received;
			return Channel.BasicConsume(Queue, false, consumer);
		}

		/// <summary>
		/// 接收消息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		protected virtual void Received(object sender, BasicDeliverEventArgs eventArgs)
		{
			Action?.Invoke(sender, eventArgs);
		}
	}
}
