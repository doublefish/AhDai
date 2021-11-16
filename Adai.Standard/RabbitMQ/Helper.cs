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
		/// 配置
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
		/// <param name="config">配置</param>
		public static void Init(Config config)
		{
			Config = config;
		}

		/// <summary>
		/// 获取连接工厂
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static IAsyncConnectionFactory GetConnectionFactory(Config config = null)
		{
			var c = config ?? Config;
			var str = $"{c.Host}-{c.Port}-{c.VirtualHost}-{c.Username}-{c.Password}";
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
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static IConnection GetConnection(Config config = null)
		{
			var factory = GetConnectionFactory(config);
			return factory.CreateConnection();
		}

		/// <summary>
		/// 声明交换器
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="config">自定义配置</param>
		public static void ExchangeDeclare(Exchange exchange, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.ExchangeDeclare(exchange);
		}

		/// <summary>
		/// 声明队列
		/// </summary>
		/// <param name="queue"></param>
		/// <param name="config">自定义配置</param>
		public static void QueueDeclare(Queue queue, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queue);
		}

		/// <summary>
		/// 声明队列并绑定到已存在的交换器
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="config">自定义配置</param>
		public static void QueueDeclareAndBind(Queue queue, string exchange, string routingKey, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclareAndBind(queue, exchange, routingKey);
		}

		/// <summary>
		/// 声明交换器和队列并绑定
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="queue">队列</param>
		/// <param name="routingKey">路由</param>
		/// <param name="config">自定义配置</param>
		public static void DeclareAndBind(Exchange exchange, Queue queue, string routingKey, Config config = null)
		{
			var factory = GetConnectionFactory(config);
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.DeclareAndBind(exchange, queue, routingKey);
		}

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="received">接收消息处理方法</param>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static string Subscribe(string queue, Func<object, BasicDeliverEventArgs, ResultType> received, Config config = null)
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
					result = received.Invoke(sender, eventArgs);
				}
				catch
				{
					result = ResultType.Exception;
				}
				channel.Ack(eventArgs.DeliveryTag, result);
			};
			return channel.BasicConsume(queue, false, consumer);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		/// <param name="config">自定义配置</param>
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
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		/// <param name="config">自定义配置</param>
		public static void Publish(string exchange, string routingKey, IBasicProperties basicProperties, string body, Config config = null)
		{
			Publish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(body), config);
		}
	}
}
