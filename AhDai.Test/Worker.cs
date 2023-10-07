using AhDai.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using System.Text;

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
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Logger.LogInformation("开始执行：{time}", DateTimeOffset.Now);
			//ElasticsearchSyncTool.Start();

			while (!stoppingToken.IsCancellationRequested)
			{
				Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				ComplexPing();
				await Task.Delay(1000, stoppingToken);
			}
		}

		public static void ComplexPing()
		{
			var pingSender = new Ping();
			var data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			var buffer = Encoding.ASCII.GetBytes(data);

			var timeout = 10000;
			var options = new PingOptions(64, true);

			var reply = pingSender.Send("www.contoso.com", timeout, buffer, options);

			if (reply.Status == IPStatus.Success)
			{
				Console.WriteLine("Address: {0}", reply.Address.ToString());
				Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
				Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
				Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
				Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
			}
			else
			{
				Console.WriteLine(reply.Status);
			}
		}

	}
}