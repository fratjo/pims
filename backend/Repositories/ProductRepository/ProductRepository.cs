using Models;

namespace Repositories.ProductRepository;


public class ProductRepository : IProductRepository
{
    public ProductRepository()
    {
        var p1 = new Product("Wireless Mouse", 25.00M, 100, "Mouse", "Ergonomic wireless mouse");
        var p2 = new Product("Mouse Pad", 10.00M, 150, "Mouse", "Smooth mouse pad");
        var p3 = new Product("Mechanical Keyboard", 70.00M, 80, "Keyboard", "RGB mechanical keyboard");
        var p4 = new Product("USB-C Hub", 40.00M, 50, "Hub", "Multi-port USB-C hub");
        var p5 = new Product("Laptop Stand", 30.00M, 75, "Asset", "Adjustable laptop stand");
        var p6 = new Product("Web Camera", 50.00M, 60, "Visio", "HD web camera for streaming");
        var p7 = new Product("Headphones", 80.00M, 40, "Audio", "Noise-canceling head phones");
        var p8 = new Product("HDMI Cable", 5.00M, 300, "Visio", "6-foot HDMI cable");
        var p9 = new Product("Wireless Charger", 20.00M, 90, "Charger", "Fast wireless charger");
        
        _products.Add(p1);
        _products.Add(p2);
        _products.Add(p3);
        _products.Add(p4);
        _products.Add(p5);
        _products.Add(p6);
        _products.Add(p7);
        _products.Add(p8);
        _products.Add(p9);
        
        var b1 = new Bundle("Productivity Bundle", "Includes a Wireless Mouse and Mouse Pad for an optimal workspace.", 30.00M, 35.00M,new List<string> { p1.Id.ToString(), p2.Id.ToString() });
        var b2 = new Bundle("Streaming Setup", "Includes a Web Camera and Headphones for the perfect streaming experience.", 100.00M, 130.00M,new List<string> { p6.Id.ToString(), p7.Id.ToString() });
        var b3 = new Bundle("Complete Laptop Kit", "Includes a Laptop Stand, USB-C Hub, and Wireless Charger to enhance your laptop experience.", 70.00M,90.00M, new List<string> { p5.Id.ToString(), p4.Id.ToString(), p9.Id.ToString() });
        
        _bundles.Add(b1);
        _bundles.Add(b2);
        _bundles.Add(b3);
    }
    
    private readonly List<Product> _products = [];
    
    private readonly List<Bundle> _bundles = [];

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await Task.FromResult<IEnumerable<Product>>(_products);
    }
    
    public async Task<Product?> GetProductById(string id)
    {
        return await Task.FromResult(_products.FirstOrDefault(p => p.Id == Guid.Parse(id)));
    }

    public async Task<IEnumerable<Bundle>> GetBundles()
    {
        return await Task.FromResult<IEnumerable<Bundle>>(_bundles);
    }

    public async Task<Bundle?> GetBundleById(string id)
    {
        return await Task.FromResult(_bundles.FirstOrDefault(b => b.Id == Guid.Parse(id)));
    }

    public async Task<IEnumerable<Bundle>> GetBundlesByProduct(string id)
    {
        return await Task.FromResult(_bundles.Where(b => b.Products != null && b.Products.Contains(id)).ToList());
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