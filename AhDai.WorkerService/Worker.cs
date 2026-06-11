using AhDai.Core.Infrastructure;
using AhDai.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.WorkerService;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    readonly IServiceProvider _serviceProvider = serviceProvider;

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
                await using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    using var ctx = new ServiceProviderScope(scope.ServiceProvider);

                    var service = scope.ServiceProvider.GetRequiredService<ITestService>();
                    await service.StartAsync();
                }
                if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("执行任务成功");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发生异常=>{time}", ex.Message);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

   
}
