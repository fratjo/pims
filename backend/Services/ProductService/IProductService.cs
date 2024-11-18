using Models;

namespace Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(string id);
    Task<string> CreateProductAsync(ProductInsertRequest product);
    Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product);
}