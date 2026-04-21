namespace ProductService.Application.Services;

public interface IProductImportService
{
    Task SyncAsync(CancellationToken cancellationToken);
}