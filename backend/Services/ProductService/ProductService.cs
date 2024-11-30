using System.Globalization;
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

    public async Task<IEnumerable<BundleResponse>> GetAllBundlesAsync()
    {
        var bundles = await repository.GetBundles();
        var responses = new List<BundleResponse>();
        
        foreach (var bundle in bundles)
        {
            var products = new List<Product>();
            foreach (var productId in bundle.Products!)
            {
                var product = await repository.GetProductById(productId);
                if (product is not null)
                {
                    products.Add(product);
                }
            }
            responses.Add(
                new BundleResponse(
                    bundle.Id, 
                    bundle.Name, 
                    bundle.Description, 
                    bundle.Price, 
                    bundle.ReelValuePrice,
                    products));
        }
        return responses;
    }

    public async Task<IEnumerable<BundleResponse>> GetBundlesByProductAsync(string id)
    {
        var productId = Guid.Parse(id);

        var bundles = await repository.GetBundlesByProduct(productId);
        
        var responses = new List<BundleResponse>();
        
        foreach (var bundle in bundles)
        {
            var products = new List<Product>();
            foreach (var pId in bundle.Products!)
            {
                var product = await repository.GetProductById(pId);
                if (product is not null)
                {
                    products.Add(product);
                }
            }
            responses.Add(
                new BundleResponse(
                    bundle.Id, 
                    bundle.Name, 
                    bundle.Description, 
                    bundle.Price, 
                    bundle.ReelValuePrice,
                    products));
        }
        
        return responses;
    }

    public async Task<BundleResponse?> GetBundleByIdAsync(string id)
    {
        var bundleId = Guid.Parse(id);
        
        var bundle = await repository.GetBundleById(bundleId);
        
        if (bundle is null) throw new NotFoundException($"Bundle with id: {id} was not found");
        
        var products = new List<Product>();
        foreach (var productId in bundle.Products!)
        {
            var product = await repository.GetProductById(productId);
            if (product is not null)
            {
                products.Add(product);
            }
        }
        
        return new BundleResponse(
            bundle.Id, 
            bundle.Name, 
            bundle.Description, 
            bundle.Price, 
            bundle.ReelValuePrice,
            products);
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
        product.Category = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(product.Category!);
        
        var newProduct = Product.CreateFromInsertRequest(product);
        
        await repository.AddProduct(newProduct);
        
        return newProduct.Id.ToString();
    }
    
    public async Task<string> CreateProductBundleAsync(BundleInsertRequest bundle)
    {
        var validationResults = BundleValidation.ValidateBundle(bundle);

        if (validationResults.Count != 0)
        {
            var errors = validationResults.Select(v => new
            {
                Field = v.MemberNames.FirstOrDefault(),
                Error = v.ErrorMessage
            });

            throw new FieldValidationException("", errors);
        }
        
        // check if products exist
        var totalPrice = 0.0;
        foreach (var bundleProduct in bundle.Products!)
        {
            var productById = await repository.GetProductById(bundleProduct);
            if (productById is null)
            {
                throw new NotFoundException("Product not found with id: " + bundleProduct);
            }

            if (productById.StockQuantity <= 0)
            {
                throw new OutOfStockException($"Product {productById.Name} is out of stock");
            }
            
            totalPrice += (double) productById.Price!;
        }
        
        // check price
        if (totalPrice * 0.6 > (double) bundle.Price!)
        {
            throw new BaseApplicationException("Bundle price must be at least 60% of the total price of the products");
        }
        
        if (totalPrice * 0.95 < (double) bundle.Price!)
        {
            throw new BaseApplicationException("Bundle price must be at most 95% of the total price of the products");
        }

        var reelValuePrice = (decimal) totalPrice;
        
        bundle.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bundle.Name!);
        
        var newBundle = Bundle.CreateFromInsertRequest(bundle, reelValuePrice);
        
        await repository.AddBundle(newBundle);
        
        return newBundle.Id.ToString();
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
