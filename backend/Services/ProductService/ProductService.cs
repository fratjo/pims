using Models;
using Repositories;

namespace Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await repository.GetProducts();
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        Guid productId = Guid.Parse(id);
        
        Product? product = await repository.GetProductById(productId);
        
        return product;
    }
}