using FluentValidation.Results;

namespace RestWithASPNET.Domain.Validation;

public class ValidationResultBag : ValidationResult
{
    public object? Data { get; set; }
}
