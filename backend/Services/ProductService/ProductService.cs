using System.Net;
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

        if (!isProductValid) throw new ValidationException("Product is invalid", HttpStatusCode.BadRequest);

        isProductValid = isProductValid && await repository.CheckIfProductNameExists(product.ProductName.ToLower());
        
        if (!isProductValid) throw new ValidationException("Product name already exists", HttpStatusCode.Conflict);
        
        var newProduct = Product.CreateFromInsertRequest(product);
        
        await repository.AddProduct(newProduct);
        
        return newProduct.Id.ToString();
    }

    public async Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product)
    {   
        var guid = Guid.Parse(id);
        
        var p = await repository.GetProductById(guid);
        
        if (p == null) throw new ValidationException("Product not found", HttpStatusCode.NotFound);
        
        var isProductValid = product
            .WithValidName()
            .WithPositivePrice()
            .WithNonNegativeStockQuantity()
            .Validate();
        
        if (!isProductValid) throw new ValidationException("Product is invalid", HttpStatusCode.BadRequest);
        
        isProductValid = isProductValid && await repository.CheckIfProductNameExists(product.ProductName.ToLower());
        
        if (!isProductValid) throw new ValidationException("Product name already exists", HttpStatusCode.Conflict);
        
        p.Update(product);
        
        await repository.UpdateProduct(p);
        
        return p;
    }
}