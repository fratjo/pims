using Models;

namespace Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(Guid id);
}