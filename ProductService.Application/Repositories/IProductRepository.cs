using ProductService.Domain.Entities;

namespace ProductService.Application.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id);
    Task<List<Product>> GetAllAsync(int pageNumber, int pageSize, Category? category = null);
}