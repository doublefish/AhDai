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
		public Models.RabbitMQConfig Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RabbitMQService(IConfiguration configuration)
		{
			Config = new Models.RabbitMQConfig()
			{
				Host = configuration.GetSection("rabbitmq:host").Value,
				VirtualHost = configuration.GetSection("rabbitmq:vhost").Value,
				Port = configuration.GetSection("rabbitmq:port").Value.ToInt32(),
				Username = configuration.GetSection("rabbitmq:username").Value,
				Password = configuration.GetSection("rabbitmq:password").Value
			};
			Utils.RabbitMQHelper.Init(Config);
		}

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IAsyncConnectionFactory GetConnectionFactory()
		{
			return Utils.RabbitMQHelper.GetConnectionFactory(Config);
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
		/// <param name="queue"></param>
		/// <param name="received"></param>
		/// <returns></returns>
		public string Subscribe(string queue, EventHandler<BasicDeliverEventArgs> received)
		{
			return Utils.RabbitMQHelper.Subscribe(queue, received, Config);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		/// <param name="basicProperties"></param>
		/// <param name="body"></param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, ReadOnlyMemory<byte> body)
		{
			Utils.RabbitMQHelper.Publish(exchange, routingKey, basicProperties, body, Config);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		/// <param name="basicProperties"></param>
		/// <param name="body"></param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, string body)
		{
			Utils.RabbitMQHelper.Publish(exchange, routingKey, basicProperties, body, Config);
		}
	}
}
