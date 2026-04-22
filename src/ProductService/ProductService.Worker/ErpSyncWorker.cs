using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Application.Services;

namespace ProductService.Worker;

public class ErpSyncWorker(IServiceScopeFactory scopeFactory, ILogger<ErpSyncWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting ERP sync...");

            try
            {
                using var scope = scopeFactory.CreateScope();
                var importService = scope.ServiceProvider.GetRequiredService<IProductImportService>();
                await importService.SyncAsync(stoppingToken);
                logger.LogInformation("ERP sync completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERP sync failed");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}