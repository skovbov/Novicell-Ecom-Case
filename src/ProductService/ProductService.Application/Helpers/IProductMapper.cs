using ProductService.Application.Queries.QueryDTOs;
using ProductService.Domain.Entities;

namespace ProductService.Application.Helpers;

public interface IProductMapper
{
    ProductDto MapToDto(Product product);
    Product MapToDomain(ProductDto productDto);
}