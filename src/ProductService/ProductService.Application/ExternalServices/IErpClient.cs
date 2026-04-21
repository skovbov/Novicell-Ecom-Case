using ProductService.Application.Queries.QueryDTOs;

namespace ProductService.Application.ExternalServices;

public interface IErpClient
{
    Task<List<ProductDto>> GetProducts();
    Task<List<CategoryDto>> GetCategories();
}
