namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;
using FluentValidation.Results;

public class AddDocumentTypeScenarioInputValidatorShould
{
    private readonly AddDocumentTypeScenarioInputValidator _validator = new();

    [Theory]
    [ClassData(typeof(AddDocumentTypeScenarioInputInvalidData))]
    public void IndicateInvalidInput(DocumentTypeInput input)
    {
        ValidationResult result = _validator.Validate(input);
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(AddDocumentTypeScenarioInputValidData))]
    public void IndicateValidaInput(DocumentTypeInput input)
    {
        ValidationResult? result = _validator.Validate(input);
        result.IsValid.Should().BeTrue();
    }
}