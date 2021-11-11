using Adai.Base.Extensions;
using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.Services
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public class RabbitMqService : Interfaces.IRabbitMqService
	{
		/// <summary>
		/// Logger
		/// </summary>
		ILogger<RabbitMqService> Logger { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger)
		{
			var config = new RabbitMQ.Config()
			{
				Host = configuration.GetSection("rabbitmq:host").Value,
				VirtualHost = configuration.GetSection("rabbitmq:vhost").Value,
				Port = configuration.GetSection("rabbitmq:port").Value.ToInt32(),
				Username = configuration.GetSection("rabbitmq:username").Value,
				Password = configuration.GetSection("rabbitmq:password").Value
			};
			RabbitMQ.Helper.Init(config);
			Logger = logger;
		}

		/// <summary>
		/// GetPublisher
		/// </summary>
		/// <returns></returns>
		public RabbitMQ.Publisher GetPublisher()
		{
			return new RabbitMQ.Publisher(Logger);
		}

		/// <summary>
		/// GetConsumer
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public RabbitMQ.Consumer GetConsumer(Action<object, BasicDeliverEventArgs> action)
		{
			return new RabbitMQ.Consumer(Logger, action);
		}
	}
}
