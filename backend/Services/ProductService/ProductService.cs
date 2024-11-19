using System.Net;
using Errors;
using Models;
using Repositories.ProductRepository;

namespace Services.ProductService;

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
        
        if (product is null) throw new NotFoundException($"Product with id: {id} was not found");
        
        return product;
    }

    public async Task<string> CreateProductAsync(ProductInsertRequest product)
    {
        var isProductValid = product
            .WithValidName()
            .WithPositivePrice()
            .WithNonNegativeStockQuantity()
            .Validate();

        if (!isProductValid) throw new FieldValidationException("Product's Fields are invalid");

        isProductValid = isProductValid && await repository.CheckIfProductNameExists(product.ProductName.ToLower());
        
        if (!isProductValid) throw new FieldConflictException("Product name already exists");
        
        var newProduct = Product.CreateFromInsertRequest(product);
        
        await repository.AddProduct(newProduct);
        
        return newProduct.Id.ToString();
    }

    public async Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product)
    {   
        var guid = Guid.Parse(id);
        
        var p = await repository.GetProductById(guid);
        
        if (p == null) throw new NotFoundException($"Product with id: {id} was not found");
        
        var isProductValid = product
            .WithValidName()
            .WithPositivePrice()
            .WithNonNegativeStockQuantity()
            .Validate();
        
        if (!isProductValid) throw new FieldValidationException("Product's Fields are invalid");
        
        isProductValid = isProductValid && await repository.CheckIfProductNameExists(product.ProductName!.ToLower());
        
        if (!isProductValid) throw new FieldConflictException("Product name already exists");
        
        p.Update(product);
        
        await repository.UpdateProduct(p);
        
        return p;
    }
}