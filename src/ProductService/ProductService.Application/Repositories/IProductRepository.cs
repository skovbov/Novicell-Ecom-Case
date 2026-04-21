using ProductService.Domain.Entities;

namespace ProductService.Application.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
}