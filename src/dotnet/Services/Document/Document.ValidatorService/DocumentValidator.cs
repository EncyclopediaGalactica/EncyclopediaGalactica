namespace EncyclopediaGalactica.Services.Document.ValidatorService;

using Entities;
using FluentValidation;

public class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        RuleSet(Operations.Add, () =>
        {
            RuleFor(p => p.Id).Equal(0);
            RuleFor(p => p.Name).NotNull().NotEmpty();
            When(p => p.Name is not null, () => { RuleFor(p => p.Name.Length).GreaterThanOrEqualTo(3); });
            RuleFor(p => p.Description).NotNull();
        });

        RuleSet(Operations.Update, () =>
        {
            RuleFor(p => p.Id).GreaterThanOrEqualTo(1);
            RuleFor(p => p.Name).NotNull().NotEmpty();
            When(p => p.Name is not null, () => { RuleFor(p => p.Name.Length).GreaterThanOrEqualTo(3); });
            RuleFor(p => p.Description).NotNull();
        });

        RuleSet(Operations.Delete, () => { RuleFor(p => p.Id).NotEqual(0); });
    }
}