using System.ComponentModel.DataAnnotations;

namespace Models;

public interface IBundle
{
    string? Name { get; set; }
    string? Description { get; set; }
    decimal? Price { get; set; }
    IEnumerable<string>? Products { get; set; }
}

public class Bundle(
    string? bundleName, 
    string? bundleDescription, 
    decimal? bundlePrice,
    decimal? bundleReelValuePrice,
    IEnumerable<string>? bundleProducts) : IBundle
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for the bundle, assigned by the system when created
    public string? Name { get; set; } = bundleName;
    public string? Description { get; set; } = bundleDescription;
    public decimal? Price { get; set; } = bundlePrice;
    public decimal? ReelValuePrice { get; set; } = bundleReelValuePrice;
    public IEnumerable<string>? Products { get; set; } = bundleProducts;
    
    public static Bundle CreateFromInsertRequest(BundleInsertRequest insertRequest, decimal bundleReelValuePrice)
    {
        var newBundle = new Bundle(
            insertRequest.Name,
            insertRequest.Description,
            insertRequest.Price,
            bundleReelValuePrice,
            insertRequest.Products
        );  
        
        return newBundle;
    }
}

public record BundleInsertRequest : IBundle
{
    [Required(ErrorMessage = "Bundle name is required")]
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Bundle price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Bundle price must be greater than zero")]
    public decimal? Price { get; set; } = null!;

    [Required(ErrorMessage = "Products are required in a bundle")]
    [MinLength(2, ErrorMessage = "A bundle must contain at least 2 products")]
    public IEnumerable<string>? Products { get; set; } = null!;
}

public class BundleResponse(
    Guid bundleId,
    string? bundleName,
    string? bundleDescription,
    decimal? bundlePrice,
    decimal? bundleReelValuePrice,
    IEnumerable<Product>? bundleProducts)
{
    public Guid Id { get; set; } = bundleId;
    public string? Name { get; set; } = bundleName;
    public string? Description { get; set; } = bundleDescription;
    public decimal? Price { get; set; } = bundlePrice;
    public decimal? ReelValuePrice { get; set; } = bundleReelValuePrice;
    public IEnumerable<Product>? Products { get; set; } = bundleProducts;
}