using ProductService.Application.ExternalServices;
using ProductService.Application.Helpers;
using ProductService.Application.Queries;
using ProductService.Application.Repositories;

namespace ProductService.Application.Services;

public class ProductImportService(IErpClient erpClient, IProductRepository productRepository, IProductMapper mapper) : IProductImportService
{
    public async Task SyncAsync(CancellationToken cancellationToken)
    {
        var erpProducts = await erpClient.GetProducts();

        foreach (var erpProduct in erpProducts)
        {
            var existing = await productRepository.GetByIdAsync(erpProduct.Id, cancellationToken);
            
            var erpProductToDomain = mapper.MapToDomain(erpProduct);
            
            if (existing == null)
                await productRepository.AddAsync(erpProductToDomain, cancellationToken);
        }
    }
}