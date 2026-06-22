using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AhDai.Service;

/// <summary>
/// Startup
/// </summary>
public static class Startup
{
    static IStartup[]? _startups;
    static IStartup[] Startups
    {
        get
        {
            _startups ??= GetStartups();
            return _startups;
        }
    }


    static IStartup[] GetStartups()
    {
        var startupType = typeof(IStartup);
        var list = new List<IStartup>();
        //var assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AhDai.Service.dll"));
        var assembly = typeof(IStartup).Assembly;
        if (assembly != null)
        {
            var types = assembly.GetTypes();
            if (types != null)
            {
                foreach (var item in types)
                {
                    if (item.IsInterface || !item.IsClass || item.IsAbstract)
                    {
                        continue;
                    }
                    var interfaces = item.GetInterfaces();
                    foreach (var i in interfaces)
                    {
                        if (i == startupType)
                        {
                            var obj = Activator.CreateInstance(item);
                            if (obj == null)
                            {
                                continue;
                            }
                            list.Add((IStartup)obj);
                        }
                    }
                }
            }
        }
        return [.. list];
    }

    /// <summary>
    /// ConfigureServices
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="isWorker"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureServices(IHostApplicationBuilder builder, IConfiguration configuration, bool isWorker = false)
    {
        try
        {
            var statuUps = Startups;
            foreach (var s in statuUps)
            {
                s.ConfigureServices(builder, isWorker);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ConfigureServices异常=>" + ex.Message);
        }
        return builder;
    }


    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="app"></param>
    public static void Configure(WebApplication app)
    {
        // 注册编码
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        try
        {
            var statuUps = Startups;
            foreach (var s in statuUps)
            {
                s.Configure(app);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Configure异常=>" + ex.Message);
        }
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="host"></param>
    public static void Configure(IHost host)
    {
        // 注册编码
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        try
        {
            var statuUps = Startups;
            foreach (var s in statuUps)
            {
                s.Configure(host);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Configure异常=>" + ex.Message);
        }
    }

}
