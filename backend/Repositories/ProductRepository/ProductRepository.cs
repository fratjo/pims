using Models;

namespace Repositories.ProductRepository;


public class ProductRepository : IProductRepository
{
    private List<Product> Products = [
        new Product("Pomme",  1.00M, 10, "Fruit"),
        new Product("Poire", 1.50M, 5, "Fruit"),
        new Product("Carotte", 0.50M, 1, "Légume"),
    ];

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await Task.FromResult<IEnumerable<Product>>(Products);
    }
    
    public async Task<Product?> GetProductById(Guid id)
    {
        return await Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
    }
    
    public async Task<bool> CheckIfProductNameExists(string newName)
    {
        var product = Products.FirstOrDefault(p => p.LowerCaseProductName == newName);
        
        return await Task.FromResult(product == null);
    }

    public async Task<IEnumerable<string>> GetCategoryNames()
    {
        return await Task.FromResult<IEnumerable<string>>(Products.Select(p => p.Category).Distinct());
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