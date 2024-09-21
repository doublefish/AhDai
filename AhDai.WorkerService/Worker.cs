using AhDai.Core.Services;
using AhDai.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.WorkerService;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            try
            {
                //await TestJwtAsync();
                await TestFileAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发生异常=>{time}", ex.Message);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    static async Task TestFileAsync()
    {
        var service = ServiceUtil.Services.GetRequiredService<IBaseFileService>();
        var dir = DateTime.Now.ToString("yyyy-MM-dd"); ;
        var url = "https://erp.ahsanle.cn/Upload/2018-08/20180806090014.xlsx";
        var name = "新四中门窗结算单 ----三乐.xlsx";
        var data = await service.DownloadAsync("D:\\ErpFiles", dir, url, name);
    }

    static async Task TestJwtAsync()
    {
        var service = ServiceUtil.Services.GetRequiredService<IBaseJwtService>();
        var tokenData = new Core.Models.TokenData()
        {
            Id = "123",
            Username = "test",
            Name = "测试",
        };
        var token = await service.GenerateTokenAsync(tokenData);
        var jwt = service.ReadToken(token.Type + " " + token.Token);
    }
}
