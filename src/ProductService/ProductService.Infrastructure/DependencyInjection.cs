using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.ExternalServices;
using ProductService.Application.Queries;
using ProductService.Infrastructure.ExternalServices;
using ProductService.Infrastructure.Helpers;
using ProductService.Infrastructure.Queries;

namespace ProductService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //MemoryCache brugt som PoC, distributed cache/redis vil være bedre til større enterprise løsninger
        services.AddMemoryCache();
        services.AddScoped<IProductQuery, ProductQuery>();
        services.AddScoped<IProductMapper, ProductMapper>();
        
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddHttpClient<IErpClient, ErpClient>(client =>
        {
            client.BaseAddress = new Uri("https://ncrecruitmentpubweu.blob.core.windows.net/cases/ecom/");
        });
        
        return services;
    }
    
}