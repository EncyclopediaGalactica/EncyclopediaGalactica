namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using FluentValidation.Results;

public class DeleteRelationTypeScenarioInputValidatorShould
{
    private readonly DeleteRelationTypeScenarioInputValidator validator = new();

    [Theory]
    [ClassData(typeof(DeleteRelationTypeScenarioInputValidatorShouldInvalidData))]
    public void Test(RelationTypeInput input, bool expectedResult)
    {
        ValidationResult validationResult = validator.Validate(input);
        validationResult.IsValid.Should().Be(expectedResult);
    }
}

[ExcludeFromCodeCoverage]
public class DeleteRelationTypeScenarioInputValidatorShouldInvalidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new RelationTypeInput { Id = 0, }, false, };
        yield return new object[] { new RelationTypeInput { Id = 1, }, true, };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}