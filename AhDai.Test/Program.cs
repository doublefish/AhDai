using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
				IHost host = Host.CreateDefaultBuilder(args)
					.ConfigureLogging((context, builder) =>
					{
						builder.AddLog4Net();
					})
					.ConfigureServices(services =>
					{
						services.AddDbService();
						services.AddHostedService<Worker>();
						ServiceUtil.Init(services.BuildServiceProvider(), null);
					})
					.Build();
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