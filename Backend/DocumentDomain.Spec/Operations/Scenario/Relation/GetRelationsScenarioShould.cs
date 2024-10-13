namespace DocumentDomain.Spec.Operations.Scenario.Relation;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;
using FluentAssertions;
using LanguageExt;

[ExcludeFromCodeCoverage]
public class GetRelationsScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnList_WhenThereAreManyItemsInTheDatabase()
    {
        int volume = 10;
        await SeedAndForgetRelations(volume);
        Either<ErrorResult, List<RelationResult>> result = await GetRelationsScenario.ExecuteAsync(
            new GetRelationsScenarioContext(Guid.NewGuid()));

        result.IsRight.Should().BeTrue();
        result.IsLeft.Should().BeFalse();
        result.IfRight(r => { r.Should().HaveCount(volume); });
    }

    [Fact]
    public async Task ReturnEmptyList_WhenThereAreNoItemsInTheDatabase()
    {
        Either<ErrorResult, List<RelationResult>> result = await GetRelationsScenario.ExecuteAsync(
            new GetRelationsScenarioContext(Guid.NewGuid()));

        result.IsRight.Should().BeTrue();
        result.IsLeft.Should().BeFalse();
        result.IfRight(r => { r.Should().BeEmpty(); });
    }
}