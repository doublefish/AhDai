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
		static object Locker { get; set; }

		/// <summary>
		/// Config
		/// </summary>
		public static Config Config { get; private set; }

		/// <summary>
		/// 连接实例
		/// </summary>
		public static IDictionary<string, IAsyncConnectionFactory> Instances { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static Helper()
		{
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
				if (Instances == null)
				{
					Instances = new Dictionary<string, IAsyncConnectionFactory>();
				}
				if (!Instances.TryGetValue(str, out var instance))
				{
					instance = new ConnectionFactory()
					{
						HostName = c.Host,
						VirtualHost = c.VirtualHost,
						Port = c.Port,
						UserName = c.Username,
						Password = c.Password
					};
					Instances.Add(str, instance);
				}
				return instance;
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
		/// 订阅队列
		/// </summary>
		/// <param name="queue"></param>
		/// <param name="received"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static string Subscribe(string queue, EventHandler<BasicDeliverEventArgs> received, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();

			// 消费者
			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += received;
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
