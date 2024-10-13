namespace DocumentDomain.Spec.Operations.Scenario.Relation;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;
using FluentAssertions;
using LanguageExt;

public class DeleteRelationScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnDefaultRelationResult_AndDelete_WhenEverythingIsOk()
    {
        int volume = 2;
        long toBeDeleted = 0;
        Dictionary<long, RelationResult> data = await SeedRelations(volume);
        Either<ErrorResult, List<RelationResult>> check = await GetRelationsScenario.ExecuteAsync(
            new GetRelationsScenarioContext(Guid.NewGuid()));
        check.IsRight.Should().BeTrue();
        check.IfRight(c =>
        {
            c.Should().HaveCount(2);
            toBeDeleted = c.First().Id;
        });

        Either<ErrorResult, RelationResult> deleteResult = await DeleteRelationScenario.ExecuteAsync(
            new DeleteRelationScenarioContext(Guid.NewGuid(), new RelationInput { Id = toBeDeleted }));
        deleteResult.IsRight.Should().BeTrue();
        deleteResult.IfRight(r => r.Id.Should().Be(0));

        Either<ErrorResult, List<RelationResult>> secondCheck = await GetRelationsScenario.ExecuteAsync(
            new GetRelationsScenarioContext(Guid.NewGuid()));
        secondCheck.IsRight.Should().BeTrue();
        secondCheck.IfRight(sc => sc.Should().HaveCount(1));
    }

    [Fact]
    public async Task ReturnErrorResult_AndDoNotDelete_WhenInputIsInvalid()
    {
        Either<ErrorResult, RelationResult> deleteResult = await DeleteRelationScenario.ExecuteAsync(
            new DeleteRelationScenarioContext(Guid.NewGuid(), new RelationInput { Id = 100 }));
        deleteResult.IsRight.Should().BeFalse();
        deleteResult.IsLeft.Should().BeTrue();
        deleteResult.IfLeft(error =>
        {
            error.CorrelationId.Should().NotBeEmpty();
            error.ErrorMessage.Should().NotBeEmpty();
        });
    }
}