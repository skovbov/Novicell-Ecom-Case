using ProductService.Application.Helpers;
using ProductService.Application.Queries.QueryDTOs;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Helpers;

public class ProductMapper : IProductMapper
{
    public ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Category = product.Category,
            Description = product.Description,
            Image = product.Image,
            Price = product.Price,
            Title = product.Title,
        };
    }
    
    public Product MapToDomain(ProductDto dto)
    {
        return new Product
        {
            Id = dto.Id,
            Category = dto.Category,
            Description = dto.Description,
            Image = dto.Image,
            Price = dto.Price,
            Title = dto.Title,
        };
    }
}