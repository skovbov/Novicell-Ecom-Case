using ProductService.Application.Queries.QueryDTOs;
using ProductService.Domain.Entities;

namespace ProductService.Application.Queries;

public interface IProductQuery
{
    Task<ProductDto?> GetByIdAsync(string id);
    Task<List<ProductDto>> GetAllAsync(int pageNumber, int pageSize);
}