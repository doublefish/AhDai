using AhDai.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AhDai.Test
{
	/// <summary>
	/// Worker
	/// </summary>
	public class Worker : BackgroundService
	{
		readonly ILogger<Worker> Logger;
		readonly IDbService DbService;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		public Worker(IConfiguration configuration, ILogger<Worker> logger, IDbService dbService)
		{
			Logger = logger;
			DbService = dbService;
			var temp = Core.Utils.ServiceHelper.GetService<IDbService>();
			var dict = Core.Utils.ConfigurationHelper.GetAll(configuration);
			foreach (var kv in dict)
			{
				Console.WriteLine($"{kv.Key}={kv.Value}");
			}
			var esConfig = configuration.GetSection("Elasticsearch");

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
			//	Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			//	await Task.Delay(1000, stoppingToken);
			//}

			Logger.LogInformation("开始执行：{time}", DateTimeOffset.Now);
			//ElasticsearchSyncTool.Start();



			return Task.CompletedTask;
		}

	}
}