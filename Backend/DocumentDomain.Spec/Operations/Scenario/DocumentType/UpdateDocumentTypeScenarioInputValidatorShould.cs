namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;
using FluentValidation.Results;

public class UpdateDocumentTypeScenarioInputValidatorShould
{
    private readonly UpdateDocumentTypeScenarioInputValidator _validator = new();

    [Theory]
    [ClassData(typeof(UpdateDocumentTypeScenarioInputInvalidData))]
    public void IndicateInputIsInvalid(DocumentTypeInput input)
    {
        ValidationResult? result = _validator.Validate(input);
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(UpdateDocumentTypeScenarioInputValidData))]
    public void IndicateInputIsValid(DocumentTypeInput input)
    {
        ValidationResult? result = _validator.Validate(input);
        result.IsValid.Should().BeTrue();
    }
}