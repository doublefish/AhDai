using AhDai.Core.Extensions;
using AhDai.Core.Utils;
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

        builder.Services.AddRedisService();
        builder.Services.AddJwtService();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();

        ServiceUtil.Init(host.Services, host.Services.GetRequiredService<IConfiguration>());

        host.Run();
    }
}