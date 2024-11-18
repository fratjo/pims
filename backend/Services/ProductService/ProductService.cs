using Errors;
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

    public async Task<string> CreateProductAsync(ProductInsertRequest product)
    {
        var isProductValid = product
            .WithValidName()
            .WithPositivePrice()
            .WithNonNegativeStockQuantity()
            .Validate();

        if (!isProductValid) throw new ValidationException("Product is invalid");

        isProductValid = isProductValid && await repository.CheckIfProductNameExists(product.ProductName.ToLower());
        
        if (!isProductValid) throw new ValidationException("Product name already exists");
        
        var newProduct = Product.CreateFromInsertRequest(product);
        
        await repository.AddProduct(newProduct);
        
        return newProduct.Id.ToString();
    }
}