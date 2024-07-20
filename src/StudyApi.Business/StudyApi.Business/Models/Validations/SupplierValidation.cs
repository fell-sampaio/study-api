using FluentValidation;
using StudyApi.Business.Models.Validations.Documents;

namespace StudyApi.Business.Models.Validations;
public class SupplierValidation : AbstractValidator<Supplier>
{
    public SupplierValidation()
    {
        RuleFor(f => f.Name)
            .NotEmpty().WithMessage("{PropertyName} field must be provided")
            .Length(2, 100)
            .WithMessage("{PropertyName} field must have between {MinLength} and {MaxLength} characters");

        When(f => f.SupplierType == SupplierType.Person, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CpfValidation.CpfLength)
                .WithMessage("Document field must be {ComparisonValue} characters long and {PropertyValue} were provided");
            RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });

        When(f => f.SupplierType == SupplierType.LegalPerson, () =>
        {
            RuleFor(f => f.Document.Length).Equal(CnpjValidation.CnpjLength)
                .WithMessage("Document field must be {ComparisonValue} characters long and {PropertyValue} were provided");
            RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                .WithMessage("The document provided is invalid");
        });
    }
}
