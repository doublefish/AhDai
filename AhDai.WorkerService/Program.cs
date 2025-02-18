using AhDai.Core.Extensions;
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

        // ��������ļ�
        builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: false, reloadOnChange: true);

        builder.Services.AddHttpClient("", client =>
        {
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            // ���ñ�����ƣ����ֻ��ʱ����Ϊ 2 Сʱ���������ֻ�������Ϊ 1 �롣
            ServicePointManager.SetTcpKeepAlive(true, 7200000, 1000);
            // Ĭ������������Ϊ2����������������
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