namespace SyncWorker.Application.ExternalServices;

public interface IErpClient
{
    Task<List<ProductDto>> GetProducts();
    Task<List<CategoryDto>> GetCategories();
}

public record ProductDto(string Id, string Title, decimal Price, string Description, string Category, string Image);
public record CategoryDto(string Id, string Name, string Description);