using System.ComponentModel.DataAnnotations;

namespace Models;

public interface IBundle
{
    string? Name { get; set; }
    string? Description { get; set; }
    decimal? Price { get; set; }
    IEnumerable<Guid>? Products { get; set; }
}

public class Bundle(
    string? bundleName, 
    string? bundleDescription, 
    decimal? bundlePrice,
    IEnumerable<Guid>? bundleProducts) : IBundle
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for the bundle, assigned by the system when created
    public string? Name { get; set; } = bundleName;
    public string? Description { get; set; } = bundleDescription;
    public decimal? Price { get; set; } = bundlePrice;
    public IEnumerable<Guid>? Products { get; set; } = bundleProducts;
    
    public static Bundle CreateFromInsertRequest(BundleInsertRequest insertRequest)
    {
        var newBundle = new Bundle(
            insertRequest.Name,
            insertRequest.Description,
            insertRequest.Price,
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
    [Range(0.01, Double.MaxValue, ErrorMessage = "Bundle price must be greater than zero")]
    public decimal? Price { get; set; } = null!;

    [Required(ErrorMessage = "Products are required in a bundle")]
    [MinLength(2, ErrorMessage = "A bundle must contain at least 2 products")]
    public IEnumerable<Guid>? Products { get; set; }
}