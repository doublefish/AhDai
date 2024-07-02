using AhDai.Core.Services;
using AhDai.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.WorkerService;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var jwtService = ServiceUtil.Services.GetRequiredService<IBaseJwtService>();
        var tokenData = new Core.Models.TokenData()
        {
            Id = "123",
            Username = "test",
            Name = "≤‚ ‘",
        };
        var token = await jwtService.GenerateTokenAsync(tokenData);
        var jwt = jwtService.ReadToken(token.Type + " " + token.Token);


        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
