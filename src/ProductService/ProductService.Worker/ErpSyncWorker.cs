using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Application.Services;

namespace ProductService.Worker;

public class ErpSyncWorker(IProductImportService importService, ILogger<ErpSyncWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting ERP sync...");
            
            try
            {
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