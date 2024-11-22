﻿using System.Globalization;
using System.Net;
using Errors;
using Models;
using Repositories.ProductRepository;

namespace Services.ProductService;

public class ProductService(IProductRepository repository) : IProductService
{
    #region Gets
    
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

    public async Task<IEnumerable<string>> GetCategoryNamesAsync()
    {
        return await repository.GetCategoryNames();
    }
    
    #endregion

    #region Posts
    
    public async Task<string> CreateProductAsync(ProductInsertRequest product)
    {
        var validationResults = ProductValidation.ValidateProduct(product);

        if (validationResults.Count != 0)
        {
            var errors = validationResults.Select(v => new
            {
                Field = v.MemberNames.FirstOrDefault(),
                Error = v.ErrorMessage
            });

            throw new FieldValidationException("", errors);
        }
        
        var isProductNameExisting = await repository.CheckIfProductNameExists(product.Name!.ToLower());
        if (!isProductNameExisting) throw new FieldConflictException("Product name already exists");
        
        product.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(product.Name);
        product.Category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(product.Category);
        
        var newProduct = Product.CreateFromInsertRequest(product);
        
        await repository.AddProduct(newProduct);
        
        return newProduct.Id.ToString();
    }
    
    #endregion

    #region Puts
    
    public async Task<Product?> UpdateProductAsync(string id, ProductUpdateRequest product)
    {   
        var guid = Guid.Parse(id);
        
        var p = await repository.GetProductById(guid);
        
        if (p == null) throw new NotFoundException($"Product with id: {id} was not found");
        
        var validationResults = ProductValidation.ValidateProduct(product);

        if (validationResults.Count != 0)
        {
            var errors = validationResults.Select(v => new
            {
                Field = v.MemberNames.FirstOrDefault(),
                Error = v.ErrorMessage
            });

            throw new FieldValidationException("", errors);
        }

        if (product.Name is not null && p.Name != product.Name)
        {
            var isProductNameExisting = await repository.CheckIfProductNameExists(product.Name.ToLower());
            if (!isProductNameExisting) throw new FieldConflictException("Product name already exists");
        }
        
        product.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(product.Name);
        product.Category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(product.Category);
        
        p.Update(product);
        
        await repository.UpdateProduct(p);
        
        return p;
    }
    
    #endregion
}
