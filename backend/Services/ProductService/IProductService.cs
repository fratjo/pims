using Models;

namespace Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(string id);
    Task<IEnumerable<Bundle>> GetAllBundlesAsync();
    Task<Bundle?> GetBundleByIdAsync(string id);
    Task<IEnumerable<string>> GetCategoryNamesAsync();
    Task<string> CreateProductAsync(ProductInsertRequest product);
    Task<string> CreateProductBundleAsync(BundleInsertRequest bundle);
    Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product);
}