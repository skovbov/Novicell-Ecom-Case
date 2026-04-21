using ProductService.Application.Queries.QueryDTOs;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Helpers;

public interface IProductMapper
{
    ProductDto MapToDto(Product product);
}