using Adai.Base.Extensions;
using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.Services
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public class RabbitMQService : Interfaces.IRabbitMQService
	{
		/// <summary>
		/// 配置
		/// </summary>
		public RabbitMQ.Config Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RabbitMQService(IConfiguration configuration)
		{
			Config = new RabbitMQ.Config()
			{
				Host = configuration.GetSection("rabbitmq:host").Value,
				Port = configuration.GetSection("rabbitmq:port").Value.ToInt32(),
				VirtualHost = configuration.GetSection("rabbitmq:vhost").Value,
				Username = configuration.GetSection("rabbitmq:username").Value,
				Password = configuration.GetSection("rabbitmq:password").Value
			};
			RabbitMQ.Helper.Init(Config);
		}

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IAsyncConnectionFactory GetConnectionFactory()
		{
			return RabbitMQ.Helper.GetConnectionFactory(Config);
		}

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IConnection GetConnection()
		{
			var factory = GetConnectionFactory();
			return factory.CreateConnection();
		}

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="recived">接收消息处理方法</param>
		/// <param name="autoStart">自动启动</param>
		/// <returns></returns>
		public string Subscribe(string queue, Func<object, BasicDeliverEventArgs, RabbitMQ.ResultType> recived, bool autoStart = true)
		{
			return RabbitMQ.Helper.Subscribe(queue, recived, autoStart, Config);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, ReadOnlyMemory<byte> body)
		{
			RabbitMQ.Helper.Publish(exchange, routingKey, basicProperties, body, Config);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, string body)
		{
			RabbitMQ.Helper.Publish(exchange, routingKey, basicProperties, body, Config);
		}
	}
}
