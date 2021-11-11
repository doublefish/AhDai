﻿using Adai.Base.Extensions;
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
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RabbitMqService(IConfiguration configuration)
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
		}

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
		/// <param name="action"></param>
		/// <returns></returns>
		public RabbitMQ.Consumer GetConsumer(ILogger logger, Action<object, BasicDeliverEventArgs> action)
		{
			return new RabbitMQ.Consumer(logger, action);
		}
	}
}
