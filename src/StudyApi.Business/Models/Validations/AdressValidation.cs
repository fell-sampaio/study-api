using FluentValidation;

namespace StudyApi.Business.Models.Validations;
public class AdressValidation : AbstractValidator<Adress>
{
    public AdressValidation()
    {
        RuleFor(c => c.PublicArea)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 200).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.District)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 100).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.ZipCode)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(8).WithMessage("{PropertyName} field must be {MaxLength} characters");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 100).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.State)
            .NotEmpty().WithMessage("Field {PropertyName} field must be provided")
            .Length(2, 50).WithMessage("Field {PropertyName} field must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Number)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(1, 50).WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");
    }
}
