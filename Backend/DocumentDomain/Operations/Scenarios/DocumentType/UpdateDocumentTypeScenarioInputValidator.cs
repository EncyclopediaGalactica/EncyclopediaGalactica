namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

/// <summary>
///     The input validator for <see cref="UpdateDocumentTypeScenario" />.
/// </summary>
public class UpdateDocumentTypeScenarioInputValidator : AbstractValidator<DocumentTypeInput>
{
    public UpdateDocumentTypeScenarioInputValidator()
    {
        RuleFor(p => p.Id).GreaterThanOrEqualTo(1);

        RuleFor(p => p.Name).NotNull();
        When(p => p.Name is not null, () =>
        {
            RuleFor(p => p.Name.Trim()).NotEmpty();
            RuleFor(p => p.Name.Trim().Length).GreaterThanOrEqualTo(3);
        });

        RuleFor(p => p.Description).NotNull();
        When(p => p.Description is not null, () =>
        {
            RuleFor(p => p.Description.Trim()).NotEmpty();
            RuleFor(p => p.Description.Trim().Length).GreaterThanOrEqualTo(3);
        });
    }
}