namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using System.Collections;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using FluentValidation.Results;

public class GetRelationTypeByIdScenarioInputValidatorShould
{
    private readonly GetRelationTypeByIdScenarioInputValidator validator = new();

    [Theory]
    [ClassData(typeof(GetRelationTypeByIdScenarioInputValidatorData))]
    public void Test(RelationTypeInput input, bool expectedResult)
    {
        ValidationResult validationResult = validator.Validate(input);
        validationResult.IsValid.Should().Be(expectedResult);
    }
}

public class GetRelationTypeByIdScenarioInputValidatorData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new RelationTypeInput { Id = 0, }, false, };
        yield return new object[] { new RelationTypeInput { Id = 1, }, true, };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}