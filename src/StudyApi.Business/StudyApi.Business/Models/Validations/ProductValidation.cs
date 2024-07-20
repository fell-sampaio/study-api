using FluentValidation;

namespace StudyApi.Business.Models.Validations;
public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 200).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 1000).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Value)
            .GreaterThan(0).WithMessage("{PropertyName} field must be greater than {ComparisonValue}");
    }
}
