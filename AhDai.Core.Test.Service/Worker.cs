using AhDai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Test.Service
{
	/// <summary>
	/// Worker
	/// </summary>
	public class Worker : BackgroundService
	{
		readonly ILogger<Worker> Logger;
		readonly Interfaces.ISubscribeService Service;
		readonly Interfaces.IRedisService RedisService;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public Worker(IConfiguration configuration, ILogger<Worker> logger, Interfaces.ISubscribeService service, Interfaces.IRedisService redisService)
		{
			Logger = logger;
			Service = service;
			RedisService = redisService;

			var dict = Core.Utils.ConfigurationHelper.GetAll(configuration);
			foreach (var kv in dict)
			{
				Console.WriteLine($"{kv.Key}={kv.Value}");
			}
		}

		/// <summary>
		/// ExecuteAsync
		/// </summary>
		/// <param name="stoppingToken"></param>
		/// <returns></returns>
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			//while (!stoppingToken.IsCancellationRequested)
			//{
			//	_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			//	await Task.Delay(1000, stoppingToken);
			//}

			var eventId = "123456789";

			Logger.Debug(eventId, "1.Worker running at: {0}", DateTimeOffset.Now);
			Logger.Information(eventId, "2.Worker running at: {0}", DateTimeOffset.Now);

			var rdb = RedisService.GetDatabase();
			var start = DateTime.Now;
			for (var i = 0; i < 1000; i++)
			{
				try
				{
					var multiplexer = Utils.RedisHelper.GetConnectionMultiplexer();
					var db = multiplexer.GetDatabase();
					var value = db.StringGet("test");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}

			}
			var ts = DateTime.Now.Subtract(start);


			var exchange = new RabbitMQ.Exchange("exchange.logistics.test")
			{
			};
			var queue = new RabbitMQ.Queue("queue.logistics.test.tag")
			{
			};

			RabbitMQ.Helper.ExchangeDeclare(exchange);
			RabbitMQ.Helper.QueueDeclare(queue);
			RabbitMQ.Helper.QueueBind(queue, exchange.Name, "tag");
			//RabbitMQ.Helper.QueueDeclareAndBind(queue, exchange.Name, "tag");

			var tag = Service.Subscribe(queue.Name, Received);

			return Task.CompletedTask;
		}

		/// <summary>
		/// 接收消息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		/// <returns></returns>
		RabbitMQ.ResultType Received(object sender, BasicDeliverEventArgs eventArgs)
		{
			var messageId = eventArgs.BasicProperties.MessageId;
			var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

			Logger.Information(messageId, $"接收消息=>{message}");
			return Core.RabbitMQ.ResultType.Success;
		}
	}
}