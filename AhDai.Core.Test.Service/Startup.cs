using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Test.Service
{
	internal static class Startup
	{
		/// <summary>
		/// StartAsync
		/// </summary>
		/// <param name="args"></param>
		public static async Task StartAsync(params string[] args)
		{
			ThreadPool.GetMinThreads(out int defaultMinThreads, out int completionPortThreads);
			Console.WriteLine($"DefaultMinThreads: {defaultMinThreads}, completionPortThreads: {completionPortThreads}");
			ThreadPool.SetMinThreads(48, 48);
			ThreadPool.GetMinThreads(out defaultMinThreads, out completionPortThreads);
			Console.WriteLine($"DefaultMinThreads: {defaultMinThreads}, completionPortThreads: {completionPortThreads}");

			try
			{
				IHost host = Host.CreateDefaultBuilder(args)
					.ConfigureHostBuilder()
					.ConfigureLogging()
					.ConfigureServices(services =>
					{
						services.AddDbService();
						services.AddRedisService();
						services.AddSubscribeService();
						services.AddPublishService();
						services.AddHostedService<Worker>();
						ServiceHelper.Init(services.BuildServiceProvider());
					})
					.Build();

				await host.RunAsync();
				Console.WriteLine($"服务启动成功=>");
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
