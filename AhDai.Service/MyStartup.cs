using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using AhDai.Integration.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace AhDai.Service;

internal class MyStartup : IStartup
{
    /// <summary>
    /// ConfigureServices
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="isWorker"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, bool isWorker = false)
    {
        //var masterConnectionString = configuration.GetConnectionString("Master");
        //ArgumentException.ThrowIfNullOrEmpty(masterConnectionString);
        //AddDbContextFactory<MasterDbContext>(services, masterConnectionString, new Interceptors.MySaveChangesInterceptor(true), new Interceptors.MyDbCommandInterceptor());

        services.AddTransient<Core.Handlers.HttpLoggingHandler>();
        services.AddHttpClient("", client =>
        {
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            //client.DefaultRequestHeaders.ExpectContinue = true;
            //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36 Edg/146.0.0.0");
            //client.DefaultRequestHeaders.Add("Accept", "*/*");
            //client.Timeout = TimeSpan.FromMinutes(5);
        }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
        {
            MaxConnectionsPerServer = 512,

            // 周期性关闭旧连接，确保能获取到 目标终端 最新的 DNS 解析结果
            PooledConnectionLifetime = TimeSpan.FromMinutes(15),

            // 如果发了探测包，30 秒内没回，就认为连接死了，立即重连
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            // 仅在有活动请求或连接池中有活跃连接时发送探测
            KeepAlivePingPolicy = HttpKeepAlivePingPolicy.WithActiveRequests,
            // 每 60 秒检查一次连接是否还活着
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),

            //Proxy = HttpClient.DefaultProxy,
            //Proxy = new WebProxy("http://127.0.0.1:8888", BypassOnLocal: true),
            //UseProxy = true,
            //SslOptions = new SslClientAuthenticationOptions
            //{
            //    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            //}

        }).AddHttpMessageHandler<Core.Handlers.HttpLoggingHandler>();
        //services.AddHttpClient("NoCertificateValidation", client =>
        //{

        //}).ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    var handler = new SocketsHttpHandler()
        //    {
        //    };
        //    handler.SslOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        //    return handler;
        //});

        services.AddHttpContextAccessor();
        services.AddRedisService();
        services.AddJwtService();
        services.AddFileService();

        services.ConfigureRedisKeyBuilder("AhDai");
        services.AddRedisKeyBuilder();
        services.AddAliyunOssService();
        services.AddAliyunOcrService();
        services.AddAliyunSmsService();
        services.AddAliyunVodService();
        services.AddAntChainNotaryService();
        services.AddAmapService();
        services.AddBaiduFaceprintService();
        services.AddBaiduMapService();
        services.AddBaiduOcrService();
        services.AddTencentMapService();
        services.AddWeChatMiniProgramService();
        services.AddWeChatOfficialAccountService();
        services.AddWeChatPayService();
        services.AddWeChatWebAppService();
        services.AddESignService();
        services.AddTianyanchaService();

        // 反射注入服务
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        AddServices(services, types);

        // 反射注入AutoMapper
        //services.AddAutoMapper(config => { }, types);

        if (isWorker)
        {
            // 注册 HangfireJobScheduler
            //services.AddHostedService<HangfireJobScheduler>();
        }
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="app"></param>
    public void Configure(WebApplication app)
    {
        // 另存服务实例
        ServiceUtil.Init(app.Services, app.Services.GetRequiredService<IConfiguration>());
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="host"></param>
    public void Configure(IHost host)
    {
        // 另存服务实例
        ServiceUtil.Init(host.Services, host.Services.GetRequiredService<IConfiguration>());
    }

    static void AddDbContextFactory<TContext>(IServiceCollection services, string connectionString, params IInterceptor[] interceptors) where TContext : DbContext
    {
        services.AddDbContextFactory<TContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString, options =>
            {
                options.UseCompatibilityLevel(120);
            });

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            options.UseLoggerFactory(loggerFactory);
            options.EnableSensitiveDataLogging();

            if (interceptors != null && interceptors.Length > 0)
            {
                options.AddInterceptors(interceptors);
            }
        }, ServiceLifetime.Singleton);
    }

    static void AddServices(IServiceCollection services, Type[] types)
    {
        if (types == null || types.Length == 0) return;
        foreach (var type in types)
        {
            if (type == null || !type.IsClass || type.IsAbstract) continue;
            var attr = type.GetCustomAttribute<Attributes.ServiceAttribute>();
            if (attr == null) continue;

            var interfaces = type.GetInterfaces();
            if (interfaces == null || interfaces.Length == 0) continue;

            var validInterfaces = interfaces.Where(iface
                => iface != null
                && !string.IsNullOrEmpty(iface.FullName)
                && !iface.FullName.StartsWith("System.")
                && !iface.FullName.StartsWith("Microsoft.")
                // 1. 这里只精准排除了基类空标记接口
                && iface != typeof(Abstractions.IBaseService)
                // 2. 这里排除的是“没有指定具体泛型泛参的原始开放定义”
                && !iface.IsGenericTypeDefinition).ToArray();
            if (validInterfaces.Length == 0) continue;

            switch (attr.Lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(type);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(type);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(type);
                    break;
            }

            foreach (var iface in validInterfaces)
            {
                switch (attr.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(iface, sp => sp.GetRequiredService(type));
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(iface, sp => sp.GetRequiredService(type));
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(iface, sp => sp.GetRequiredService(type));
                        break;
                }
            }
        }
    }
}
