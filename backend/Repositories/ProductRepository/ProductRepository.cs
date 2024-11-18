using Models;

namespace Repositories;


public class ProductRepository : IProductRepository
{
    private List<Product> Products = [
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

    public Task<bool> CheckIfProductNameExists(string name)
    {
        var product = Products.FirstOrDefault(p => p.LowerCaseProductName == name);
        
        return Task.FromResult(product == null);
    }

    public async Task<bool> AddProduct(Product product)
    {
        this.Products.Add(product);
        
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        int index = this.Products.FindIndex(p => p.Id == product.Id);

        if (index != -1)
        {
            this.Products[index] = product;
            return await Task.FromResult(true);
        }
        
        return await Task.FromResult(false);
    }
}