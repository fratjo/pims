namespace Models;

public class Product(string productName, string? productDescription, decimal productPrice, int productStockQuantity)
{ 
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = productName;
    public string? Description { get; set; } = productDescription;
    public decimal Price { get; set; } = productPrice;
    public int StockQuantity { get; set; } = productStockQuantity;
}

public record ProductInsertRequest(string productName, string? productDescription, decimal productPrice, int productStockQuantity) {}

public record ProductUpdateRequest(string? productName, string? productDescription, decimal? productPrice, int? productStockQuantity);
