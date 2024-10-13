namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using LanguageExt;

public class AddRelationTypeScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(AddRelationTypeScenarioInputValidationInvalidInputData))]
    public async Task ShowValidState_AndReturnResult_WhenInputIsValid(RelationTypeInput input)
    {
        Either<ErrorResult, RelationTypeResult> result = await AddRelationTypeScenario.ExecuteAsync(
            new AddRelationTypeScenarioContext(Guid.NewGuid(), input));
        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(r =>
        {
            r.Should().NotBeNull();
            r.CorrelationId.Should().NotBeEmpty();
            r.ErrorMessage.Should().NotBeNull();
            r.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ShowInvalidState_AndReturnErrorResult_WhenInputIsInvalid()
    {
        RelationTypeInput input = new() { Id = 0, Name = "Name", Description = "description", };
        Either<ErrorResult, RelationTypeResult> result = await AddRelationTypeScenario.ExecuteAsync(
            new AddRelationTypeScenarioContext(Guid.NewGuid(), input));

        result.IsRight.Should().BeTrue();
        result.IsLeft.Should().BeFalse();
        result.IfRight(res =>
        {
            res.Should().NotBeNull();
            res.Id.Should().BeGreaterThanOrEqualTo(1);
            res.Name.Should().Be(input.Name);
            res.Description.Should().Be(input.Description);
        });
    }
}