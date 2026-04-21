using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductService.Application.Queries;
using ProductService.Application.Queries.QueryDTOs;
using ProductService.Infrastructure.Helpers;

namespace ProductService.Infrastructure.Queries;

public class ProductQuery(ProductDbContext dbContext, IProductMapper mapper, IMemoryCache cache) : IProductQuery
{
    public async Task<ProductDto?> GetByIdAsync(string id)
    {
        var cacheKey = $"product:{id}";
        
        if (cache.TryGetValue(cacheKey, out ProductDto? cached))
            return cached;
        
        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        var dto = mapper.MapToDto(product);
        cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
        return dto;
    }

    public async Task<List<ProductDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var cacheKey = $"products:{pageNumber}:{pageSize}";

        if (cache.TryGetValue(cacheKey, out List<ProductDto>? cached))
            return cached!;
        
        var query = dbContext.Products
            .AsNoTracking()
            .AsQueryable();

        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dtos = products.Select(mapper.MapToDto).ToList();
        cache.Set(cacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}