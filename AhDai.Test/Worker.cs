using AhDai.Core.Extensions;
using AhDai.Core.Services;
using AhDai.Core.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Test
{
	/// <summary>
	/// Worker
	/// </summary>
	public class Worker : BackgroundService
	{
		readonly ILogger<Worker> _logger;
		readonly IConfiguration _configuration;
		readonly IDbService _dbService;

		public Worker(ILogger<Worker> logger, IConfiguration configuration, IDbService dbService)
		{
			_logger = logger;
			_configuration = configuration;
			_dbService = dbService;
			var dict = Core.Utils.ConfigurationUtil.GetAll(configuration);
			foreach (var kv in dict)
			{
				Console.WriteLine($"{kv.Key}={kv.Value}");
			}
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("开始执行：{time}", DateTimeOffset.Now);

			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

				//ComplexPing();
				//TestKafka();
				TestDb();

				await Task.Delay(3000, stoppingToken);
			}
		}


		protected virtual void TestDb()
		{
			var dbConfigs = _configuration.GetDbConfigs();
			var dbContext = _dbService.GetSqlDbContext("");
		}

		protected virtual void TestEs()
		{
			var esConfig = _configuration.GetSection("Elasticsearch");
			ElasticsearchSyncTool.Start();
		}

		protected virtual void TestKafka()
		{

		}


		protected virtual void ComplexPing()
		{
			var pingSender = new Ping();
			var data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			var buffer = Encoding.ASCII.GetBytes(data);

			var options = new PingOptions(64, true);

			var reply = pingSender.Send("www.contoso.com", 5000, buffer, options);

			if (reply.Status == IPStatus.Success)
			{
				Console.WriteLine($"来自 {reply.Address} 的回复: 字节={reply.Buffer.Length} 时间={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");

				//Console.WriteLine("Address: {0}", reply.Address.ToString());
				//Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
				//Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
				//Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
				//Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
			}
			else
			{
				Console.WriteLine(reply.Status);
			}
		}
	}
}