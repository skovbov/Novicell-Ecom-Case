using System.Net.Http.Json;
using SyncWorker.Application.ExternalServices;

namespace SyncWorker.Infrastructure.ExternalServices;

public class ErpClient(HttpClient httpClient) : IErpClient
{
    public async Task<List<ProductDto>> GetProducts()
    {
        var response = await httpClient.GetFromJsonAsync<List<ProductDto>>("products-sample-v1.json");

        return response ?? [];
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var response = await httpClient.GetFromJsonAsync<List<CategoryDto>>("categories-sample-v1.json");
        
        return response ?? [];
    }
}