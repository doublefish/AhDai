using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace AhDai.WorkerService;

public class Program
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppContext.BaseDirectory);

        var builder = Host.CreateApplicationBuilder(args);

        // 添加配置文件
        builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: false, reloadOnChange: true);

        // 添加业务服务
        Service.Startup.ConfigureServices(builder.Services, builder.Configuration, true);

        //builder.Services.AddRedisService();
        //builder.Services.AddJwtService();
        //builder.Services.AddFileService();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        Service.Startup.Configure(host);

        host.Run();
    }


}