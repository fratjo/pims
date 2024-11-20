using System.ComponentModel.DataAnnotations;

namespace Models;

public class NotEmptyOrWhitespaceIfNotNullAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success; // Null est autorisé
        }

        if (value is string str && string.IsNullOrWhiteSpace(str))
        {
            return new ValidationResult("The field cannot be empty or whitespace if provided.");
        }

        return ValidationResult.Success;
    }
}