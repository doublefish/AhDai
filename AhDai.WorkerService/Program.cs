using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;

namespace AhDai.WorkerService;

public class Program
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppContext.BaseDirectory);

        var builder = Host.CreateApplicationBuilder(args);

        // 添加配置文件
        builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: false, reloadOnChange: true);

        builder.Services.AddHttpClient("", client =>
        {
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            // 启用保活机制：保持活动超时设置为 2 小时，并将保持活动间隔设置为 1 秒。
            ServicePointManager.SetTcpKeepAlive(true, 7200000, 1000);
            // 默认连接数限制为2，增加连接数限制
            ServicePointManager.DefaultConnectionLimit = 512;
        });//.AddHttpMessageHandler<Handlers.HttpLoggingHandler>();

        //builder.Services.AddRedisService();
        //builder.Services.AddJwtService();
        //builder.Services.AddFileService();
        //builder.Services.AddHostedService<Worker>();
        builder.Services.AddHostedService<Worker2>();

        var host = builder.Build();

        ServiceUtil.Init(host.Services, host.Services.GetRequiredService<IConfiguration>());

        host.Run();
    }
}