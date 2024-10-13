namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using LanguageExt;

public class GetRelationTypeByIdScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(GetRelationTypeByIdScenarioInputValidatorData))]
    public async Task ReturnInvalid_WhenInputIsInvalid(RelationTypeInput input, bool expectedResult)
    {
        Either<ErrorResult, RelationTypeResult> result = await GetRelationTypeByIdScenario.ExecuteAsync(
            new GetRelationTypeByIdScenarioContext(Guid.NewGuid(), input));
        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(e =>
        {
            e.Should().NotBeNull();
            e.CorrelationId.Should().NotBeEmpty();
            e.ErrorMessage.Should().NotBeNull();
            e.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnValid_AndResult_WhenInputIsValid()
    {
        RelationTypeInput input = new() { Id = 0, Name = "asdf", Description = "asdf", };
        Either<ErrorResult, RelationTypeResult> data = await AddRelationTypeScenario.ExecuteAsync(
            new AddRelationTypeScenarioContext(Guid.NewGuid(), input));
        data.IsRight.Should().BeTrue();

        RelationTypeInput mod = new();
        data.IfRight(d => { mod.Id = d.Id; });
        Either<ErrorResult, RelationTypeResult> modResult = await GetRelationTypeByIdScenario.ExecuteAsync(
            new GetRelationTypeByIdScenarioContext(Guid.NewGuid(), mod));

        modResult.IsLeft.Should().BeFalse();
        modResult.IsRight.Should().BeTrue();
        modResult.IfRight(r =>
        {
            r.Id.Should().Be(mod.Id);
            r.Name.Should().Be(input.Name);
            r.Description.Should().Be(input.Description);
        });
    }
}