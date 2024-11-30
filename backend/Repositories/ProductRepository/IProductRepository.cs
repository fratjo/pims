using Models;

namespace Repositories.ProductRepository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(string id);
    Task<IEnumerable<Bundle>> GetBundles();
    Task<Bundle?> GetBundleById(string id);
    Task<IEnumerable<Bundle>> GetBundlesByProduct(string id);
    Task<bool> CheckIfProductNameExists(string newName);
    Task<IEnumerable<string>> GetCategoryNames();
    Task<bool> AddProduct(Product product);
    Task<bool> AddBundle(Bundle bundle);
    Task<bool> UpdateProduct(Product product);
}