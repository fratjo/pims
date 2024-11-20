﻿using System.ComponentModel.DataAnnotations;

namespace Models;

public interface IProduct
{
    string? Name { get; set; }
    string? Description { get; set; }
    decimal? Price { get; set; }
    int? StockQuantity { get; set; }
    
}

public class Product(
    string? productName, 
    string? productDescription, 
    decimal? productPrice, 
    int? productStockQuantity) : IProduct
{ 
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; } = productName;
    public string LowerCaseProductName { get; private set; } = productName!.ToLower();
    public string? Description { get; set; } = productDescription;
    public decimal? Price { get; set; } = productPrice;
    public int? StockQuantity { get; set; } = productStockQuantity;

    public static Product CreateFromInsertRequest(ProductInsertRequest insertRequest)
    {
        return new Product(
            insertRequest.Name, 
            insertRequest.Description, 
            insertRequest.Price, 
            insertRequest.StockQuantity);
    }

    public void Update(ProductUpdateRequest productUpdateRequest)
    {
        Name = productUpdateRequest.Name ?? Name;
        LowerCaseProductName = Name!.ToLower();
        Description = productUpdateRequest.Description ?? Description;
        Price = productUpdateRequest.Price ?? Price;
        StockQuantity = productUpdateRequest.StockQuantity ?? StockQuantity;
    }
}

public record ProductInsertRequest : IProduct
{
    [Required(ErrorMessage = "Product name is required")]
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Product price is required")]
    [Range(0.01, Double.MaxValue, ErrorMessage = "Product price must be greater than zero")]
    public decimal? Price { get; set; } = null;

    [Required(ErrorMessage = "Product stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Product stock quantity must be zero or positive")]
    public int? StockQuantity { get; set; } = null;
}

public record ProductUpdateRequest : IProduct
{
    [NotEmptyOrWhitespaceIfNotNull(ErrorMessage = "Product name must not be empty or whitespace.")]
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero")]
    public decimal? Price { get; set; } = null;
    
    [Range(0, int.MaxValue, ErrorMessage = "Product stock quantity must be zero or positive")]
    public int? StockQuantity { get; set; } = null;
};
