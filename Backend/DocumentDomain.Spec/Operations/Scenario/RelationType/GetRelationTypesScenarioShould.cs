namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using System.Collections.Immutable;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using LanguageExt;
using Xunit.Abstractions;

public class GetRelationTypesScenarioShould(ITestOutputHelper testOutputHelper) : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnSuccess_AndEmptyResult_WhenTheDatabaseIsEmpty()
    {
        Either<ErrorResult, ImmutableList<RelationTypeResult>> result = await GetRelationTypesScenario.ExecuteAsync(
            new GetRelationTypesScenarioContext(Guid.NewGuid()));
        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r =>
        {
            r.Should().NotBeNull();
            r.Should().BeEmpty();
        });
    }

    [Fact]
    public async Task ReturnSuccess_AndResultList_WhenTheDatabaseIsNotEmpty()
    {
        await SeedAndForgetRelationTypes(4, testOutputHelper);
        Either<ErrorResult, ImmutableList<RelationTypeResult>> result = await GetRelationTypesScenario.ExecuteAsync(
            new GetRelationTypesScenarioContext(Guid.NewGuid()));
        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r =>
        {
            r.Should().NotBeNull();
            r.Should().HaveCount(4);
        });
    }
}