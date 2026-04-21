using Microsoft.EntityFrameworkCore;
using ProductService.Application.Repositories;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
}