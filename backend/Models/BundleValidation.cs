using System.ComponentModel.DataAnnotations;

namespace Models;

public static class BundleValidation
{
    public static List<ValidationResult> ValidateBundle(IBundle bundle)
    {
        var errors = new List<ValidationResult>();
        var context = new ValidationContext(bundle);
        
        Validator.TryValidateObject(bundle, context, errors, true);
        
        return errors;
    }
}