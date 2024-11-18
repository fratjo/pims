using Models;

namespace Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(Guid id);
    Task<bool> CheckIfProductNameExists(string name);
    Task<bool> AddProduct(Product product);
    Task<bool> UpdateProduct(Product product);
}