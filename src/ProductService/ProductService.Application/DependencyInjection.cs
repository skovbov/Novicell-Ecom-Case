using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Services;

namespace ProductService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductImportService, ProductImportService>();
        
        return services;
    }
}