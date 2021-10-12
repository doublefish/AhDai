using Adai.Extensions;
using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Adai.Standard.Services
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public class RabbitMqService : Interfaces.IRabbitMqService
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RabbitMqService(IConfiguration configuration)
		{
			Configuration = configuration;
			var config = new Models.RabbitMqConfig()
			{
				Host = configuration.GetSection("rabbitmq:host").Value,
				VirtualHost = configuration.GetSection("rabbitmq:vhost").Value,
				Port = configuration.GetSection("rabbitmq:port").Value.ToInt32(),
				Username = configuration.GetSection("rabbitmq:username").Value,
				Password = configuration.GetSection("rabbitmq:password").Value
			};
			Utils.RabbitMQHelper.Init(config);
		}

		/// <summary>
		/// Configuration
		/// </summary>
		public IConfiguration Configuration { get; private set; }

		/// <summary>
		/// GetPublisher
		/// </summary>
		/// <param name="logger"></param>
		/// <returns></returns>
		public RabbitMQ.Publisher GetPublisher(ILogger logger)
		{
			return new RabbitMQ.Publisher(logger);
		}

		/// <summary>
		/// GetConsumer
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="func"></param>
		/// <returns></returns>
		public RabbitMQ.Consumer GetConsumer(ILogger logger, Func<string, bool> func)
		{
			return new RabbitMQ.Consumer(logger, func);
		}
	}
}
