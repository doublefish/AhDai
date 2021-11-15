using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// RabbitMQHelper
	/// </summary>
	public static class Helper
	{
		static readonly IDictionary<string, IAsyncConnectionFactory> Factories;
		static readonly object Locker;

		/// <summary>
		/// Config
		/// </summary>
		public static Config Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static Helper()
		{
			Factories = new Dictionary<string, IAsyncConnectionFactory>();
			Locker = new object();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config"></param>
		public static void Init(Config config)
		{
			Config = config;
		}

		/// <summary>
		/// 获取连接工厂
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IAsyncConnectionFactory GetConnectionFactory(Config config = null)
		{
			var c = config ?? Config;
			var str = $"{c.Host}-{c.VirtualHost}-{c.Port}-{c.Username}-{c.Password}";
			lock (Locker)
			{
				if (!Factories.TryGetValue(str, out var factory))
				{
					factory = new ConnectionFactory()
					{
						HostName = c.Host,
						Port = c.Port,
						VirtualHost = c.VirtualHost,
						UserName = c.Username,
						Password = c.Password
					};
					Factories.Add(str, factory);
				}
				return factory;
			}
		}

		/// <summary>
		/// 获取连接工厂
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IConnection GetConnection(Config config = null)
		{
			var factory = GetConnectionFactory(config);
			return factory.CreateConnection();
		}

		/// <summary>
		/// 队列初始化（声明交换器+声明队列+绑定队列）
		/// </summary>
		/// <param name="queue"></param>
		/// <param name="routingKey"></param>
		/// <param name="exchange"></param>
		/// <param name="config"></param>
		public static void QueueInit(string queue, string routingKey, Exchange exchange, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.ExchangeDeclare(exchange.Name, exchange.Type.GetString(), exchange.Durable, exchange.AutoDelete);
			channel.QueueDeclare(queue, true, false, false);
			channel.QueueBind(queue, exchange.Name, routingKey);
		}

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue"></param>
		/// <param name="excute"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static string Subscribe(string queue, Func<object, BasicDeliverEventArgs, ResultType> excute, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();

			// 消费者
			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (sender, eventArgs) =>
			{
				ResultType result;
				try
				{
					result = excute.Invoke(sender, eventArgs);
				}
				catch
				{
					result = ResultType.Exception;
				}
				switch (result)
				{
					case ResultType.Success:
						// 业务处理成功，从队列中移除
						channel.BasicAck(eventArgs.DeliveryTag, false);
						break;
					case ResultType.Fail:
						// 从队列中移除
						channel.BasicNack(eventArgs.DeliveryTag, false, false);
						break;
					case ResultType.Retry:
						// 添加到队列尾部
						channel.BasicNack(eventArgs.DeliveryTag, false, true);
						break;
					case ResultType.Exception:
						// 从队列中移除
						channel.BasicNack(eventArgs.DeliveryTag, false, false);
						break;
					default:
						goto case ResultType.Fail;
				}
			};
			return channel.BasicConsume(queue, false, consumer);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		/// <param name="basicProperties"></param>
		/// <param name="body"></param>
		/// <param name="config"></param>
		public static void Publish(string exchange, string routingKey, IBasicProperties basicProperties, ReadOnlyMemory<byte> body, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.BasicPublish(exchange, routingKey, basicProperties, body);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		/// <param name="basicProperties"></param>
		/// <param name="body"></param>
		/// <param name="config"></param>
		public static void Publish(string exchange, string routingKey, IBasicProperties basicProperties, string body, Config config = null)
		{
			Publish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(body), config);
		}
	}
}
