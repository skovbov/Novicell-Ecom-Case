using SyncWorker.Application.ExternalServices;

namespace SyncWorker.Application.Repositories;

public interface IProductRepository
{
    Task <List<ProductDto>> GetProducts();
}