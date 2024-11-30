using Models;

namespace Repositories.ProductRepository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(Guid id);
    Task<IEnumerable<Bundle>> GetBundles();
    Task<Bundle?> GetBundleById(Guid id);
    Task<IEnumerable<Bundle>> GetBundlesByProduct(Guid id);
    Task<bool> CheckIfProductNameExists(string newName);
    Task<IEnumerable<string>> GetCategoryNames();
    Task<bool> AddProduct(Product product);
    Task<bool> AddBundle(Bundle bundle);
    Task<bool> UpdateProduct(Product product);
}