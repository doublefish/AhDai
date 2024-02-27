using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;

namespace AhDai.Test
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ThreadPool.GetMinThreads(out int defaultMinThreads, out int completionPortThreads);
			Console.WriteLine($"DefaultMinThreads: {defaultMinThreads}, completionPortThreads: {completionPortThreads}");
			ThreadPool.SetMinThreads(48, 48);
			ThreadPool.GetMinThreads(out defaultMinThreads, out completionPortThreads);
			Console.WriteLine($"DefaultMinThreads: {defaultMinThreads}, completionPortThreads: {completionPortThreads}");

			try
			{
				var builder = Host.CreateApplicationBuilder(args);

				var configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json").Build();

				builder.Services.AddSingleton<IConfiguration>(configuration);

				builder.Logging.AddLog4Net();

				// 添加业务服务
				builder.Services.AddDbService();
				//Service.Startup.ConfigureServices(builder.Services);

				builder.Services.AddHostedService<Worker>();

				var host = builder.Build();

				// 
				ServiceUtil.Init(host.Services, host.Services.GetRequiredService<IConfiguration>());

				Console.WriteLine("服务启动开始");
				host.Run();
				Console.WriteLine($"服务启动成功=>{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"服务启动失败=>{ex.Message}\r\n{ex.StackTrace}");
			}

			Console.WriteLine($"Press any key to continue...");
			var input = Console.ReadKey();
		}
	}
}