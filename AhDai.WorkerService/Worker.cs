using AhDai.Core.Services;
using AhDai.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Cryptography;
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
                await GenerateRsaAsync();
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
        Console.WriteLine(data.ActualName);
    }

    static async Task GenerateRsaAsync()
    {
        using var rsa = RSA.Create(2048);
        var privateKeyBytes = rsa.ExportRSAPrivateKey();
        var publicKeyBytes = rsa.ExportRSAPublicKey();

        var privateKey = Convert.ToBase64String(privateKeyBytes);
        var publicKey = Convert.ToBase64String(publicKeyBytes);

        using var rsa1 = RSA.Create();
        rsa1.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

        using var rsa2 = RSA.Create();
        rsa2.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);


        await File.WriteAllTextAsync("private_key.pem", privateKey);
        await File.WriteAllTextAsync("public_key.pem", publicKey);

        Console.WriteLine("Private and public keys saved.");
    }
}
