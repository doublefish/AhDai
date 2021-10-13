using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 消费者
	/// </summary>
	public class Consumer : Basic
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="func"></param>
		public Consumer(ILogger logger, Func<string, bool> func) : base(logger)
		{
			Func = func;
		}

		/// <summary>
		/// 业务方法
		/// </summary>
		readonly Func<string, bool> Func;

		/// <summary>
		/// 订阅消息
		/// </summary>
		public void Subscribe()
		{
			Init();

			// 消费者
			var consumer = new EventingBasicConsumer(Channel);
			// 绑定
			Channel.QueueBind(Queue, Exchange, RoutingKey);
			consumer.Received += Received;
			Channel.BasicConsume(Queue, false, consumer);
		}

		/// <summary>
		/// 接收消息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ea"></param>
		void Received(object sender, BasicDeliverEventArgs ea)
		{
			var message = string.Empty;
			try
			{
				message = Encoding.UTF8.GetString(ea.Body.ToArray());
				// 执行业务处理
				if (Func.Invoke(message))
				{
					// 业务处理成功，删除队列
					Channel.BasicAck(ea.DeliveryTag, false);
				}
				else
				{
					Logger.LogError($"消息处理失败=>{message}");
					if (ForwardFailure)
					{
						// 失败后转发消息到失败队列
						var properties = Channel.CreateBasicProperties();
						properties.Headers = new ConcurrentDictionary<string, object>
						{
							["origin-exchange"] = ea.Exchange,
							["origin-routingkey"] = ea.RoutingKey
						};
						Channel.BasicPublish(ea.Exchange, $"{ea.RoutingKey}@error", properties, ea.Body);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, $"消息处理异常=>{message}");
				throw;
			}
		}
	}
}
