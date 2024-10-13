namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

/// <summary>
///     Input data validator of the <see cref="DeleteDocumentTypeScenario" />.
/// </summary>
public class DeleteDocumentTypeScenarioInputValidator : AbstractValidator<DocumentTypeInput>
{
    public DeleteDocumentTypeScenarioInputValidator()
    {
        RuleFor(p => p.Id).GreaterThanOrEqualTo(1);
    }
}