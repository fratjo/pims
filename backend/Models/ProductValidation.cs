using System.ComponentModel.DataAnnotations;

namespace Models;

public static class ProductValidation
{
    public static List<ValidationResult> ValidateProduct(IProduct product)
    {
        var errors = new List<ValidationResult>();
        var context = new ValidationContext(product);
        
        Validator.TryValidateObject(product, context, errors, true);
        
        return errors;
    }
}