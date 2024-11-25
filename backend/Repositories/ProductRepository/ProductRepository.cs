using Models;

namespace Repositories.ProductRepository;


public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = [
        new Product("Pomme",  1.00M, 10, "Fruit"),
        new Product("Poire", 1.50M, 5, "Fruit"),
        new Product("Carotte", 0.50M, 1, "Légume"),
    ];
    
    private readonly List<Bundle> _bundles = [];

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await Task.FromResult<IEnumerable<Product>>(_products);
    }
    
    public async Task<Product?> GetProductById(Guid id)
    {
        return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    }

    public async Task<IEnumerable<Bundle>> GetBundles()
    {
        return await Task.FromResult<IEnumerable<Bundle>>(_bundles);
    }

    public async Task<Bundle?> GetBundleById(Guid id)
    {
        return await Task.FromResult(_bundles.FirstOrDefault(b => b.Id == id));
    }

    public async Task<bool> CheckIfProductNameExists(string newName)
    {
        var product = _products.FirstOrDefault(p => p.LowerCaseProductName == newName);
        
        return await Task.FromResult(product == null);
    }

    public async Task<IEnumerable<string>> GetCategoryNames()
    {
        return await Task.FromResult<IEnumerable<string>>(_products.Select(p => p.Category).Distinct()!);
    }

    public async Task<bool> AddProduct(Product product)
    {
        this._products.Add(product);
        
        return await Task.FromResult(true);
    }

    public async Task<bool> AddBundle(Bundle bundle)
    {
        this._bundles.Add(bundle);
        
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        int index = this._products.FindIndex(p => p.Id == product.Id);

        if (index != -1)
        {
            this._products[index] = product;
            return await Task.FromResult(true);
        }
        
        return await Task.FromResult(false);
    }
}