namespace DocumentDomain.Spec.Operations.Scenario.Relation;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;
using FluentAssertions;
using LanguageExt;

public class GetRelationByIdScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnTheItem()
    {
        RelationInput input = new RelationInput { Id = 0, LeftEndId = 100, RightEndId = 50 };
        Either<ErrorResult, RelationResult> inputResult = await AddRelationScenario.ExecuteAsync(
            new AddRelationScenarioContext(Guid.NewGuid(), input));
        RelationResult recordedInput = new();
        inputResult.IfRight(r => { recordedInput.Id = r.Id; });

        Either<ErrorResult, RelationResult> result = await GetRelationByIdScenario.ExecuteAsync(
            new GetRelationByIdScenarioContext(Guid.NewGuid(), new RelationInput { Id = recordedInput.Id }));

        result.IsRight.Should().BeTrue();
        result.IsLeft.Should().BeFalse();
        result.IfRight(r => { r.Id.Should().Be(recordedInput.Id); });
    }

    [Fact]
    public async Task ReturnErrorResult_WhenThereIsNoSuchItem()
    {
        Either<ErrorResult, RelationResult> result = await GetRelationByIdScenario.ExecuteAsync(
            new GetRelationByIdScenarioContext(Guid.NewGuid(), new RelationInput { Id = 11 }));

        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(error =>
        {
            error.CorrelationId.Should().NotBeEmpty();
            error.ErrorMessage.Should().NotBeEmpty();
        });
    }
}