using Models;

namespace Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(string id);
    Task<string> CreateProductAsync(ProductInsertRequest product);
    Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product);
}