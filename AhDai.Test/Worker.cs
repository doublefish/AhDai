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

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="logger"></param>
		public Worker(ILogger<Worker> logger)
		{
			Logger = logger;
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

			Logger.LogInformation("��ʼִ�У�{time}", DateTimeOffset.Now);
			ElasticsearchSyncTool.Start();

			return Task.CompletedTask;
		}

	}
}