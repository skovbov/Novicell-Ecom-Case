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
            CategoryId = product.CategoryId,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Title = product.Title,
        };
    }
}