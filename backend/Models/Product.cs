namespace Models;

public class Product(string productName, string? productDescription, decimal productPrice, int productStockQuantity)
{ 
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = productName;
    public string LowerCaseProductName { get; set; } = productName.ToLower();
    public string? Description { get; set; } = productDescription;
    public decimal Price { get; set; } = productPrice;
    public int StockQuantity { get; set; } = productStockQuantity;

    public static Product CreateFromInsertRequest(ProductInsertRequest insertRequest)
    {
        return new Product(
            insertRequest.ProductName, 
            insertRequest.ProductDescription, 
            insertRequest.ProductPrice, 
            insertRequest.ProductStockQuantity);
    }
}

public record ProductInsertRequest(
    string ProductName,
    string? ProductDescription,
    decimal ProductPrice,
    int ProductStockQuantity)
{
    private bool _isValid = true;
    public ProductInsertRequest WithValidName()
    {
        _isValid = _isValid && !string.IsNullOrWhiteSpace(ProductName);
        return this;
    }

    public ProductInsertRequest WithPositivePrice()
    {
        _isValid = _isValid && ProductPrice > 0;
        return this;
    }

    public ProductInsertRequest WithNonNegativeStockQuantity()
    {
        _isValid = _isValid && ProductStockQuantity >= 0;
        return this;
    }

    public bool Validate()
    {
        return _isValid;
    }
}

public record ProductUpdateRequest(string? ProductName, string? ProductDescription, decimal? ProductPrice, int? ProductStockQuantity);
