using ProductService.Application.Queries.QueryDTOs;

namespace ProductService.Application.Queries;

public interface IProductQuery
{
    Task<ProductDto?> GetByIdAsync(string id);
    Task<List<ProductDto>> GetAllAsync(int pageNumber, int pageSize, string? searchName = null);
}