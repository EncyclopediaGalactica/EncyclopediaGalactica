namespace DocumentDomain.Spec.Operations.Scenario;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators;
using FluentAssertions;
using FluentValidation.Results;

public class AddDocumentScenarioInputValidatorShould
{
    private readonly AddDocumentScenarioInputValidator _addDocumentScenarioInputValidator = new();

    [Theory]
    [ClassData(typeof(AddDocumentSagaInputInvalidData))]
    public void IndicateTheInputIsInvalid(DocumentInput documentInput)
    {
        ValidationResult? res = _addDocumentScenarioInputValidator.Validate(documentInput);
        res.IsValid.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(AddDocumentSagaInputValidData))]
    public void IndicateTheInputIsValid(DocumentInput documentInput)
    {
        ValidationResult? res = _addDocumentScenarioInputValidator.Validate(documentInput);
        res.IsValid.Should().BeTrue();
    }
}