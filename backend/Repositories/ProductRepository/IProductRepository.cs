using Models;

namespace Repositories.ProductRepository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(Guid id);
    Task<bool> CheckIfProductNameExists(string newName);
    Task<bool> AddProduct(Product product);
    Task<bool> UpdateProduct(Product product);
}