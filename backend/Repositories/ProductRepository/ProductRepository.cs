using Models;

namespace Repositories;


public class ProductRepository : IProductRepository
{
    private static List<Product> Products = [
        new Product("Pomme", "Fruit", 1.00M, 10),
        new Product("Poire", "Fruit", 1.50M, 5),
    ];

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await Task.FromResult<IEnumerable<Product>>(Products);
    }
    
    public async Task<Product?> GetProductById(Guid id)
    {
        return await Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
    }
}