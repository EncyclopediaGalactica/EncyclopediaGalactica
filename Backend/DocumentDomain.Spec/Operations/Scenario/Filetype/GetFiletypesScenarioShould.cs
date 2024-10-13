namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using LanguageExt;
using Xunit.Abstractions;

public class GetFiletypesScenarioShould(ITestOutputHelper _testOutputHelper) : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnEmptyList_WhenThereIsNoData()
    {
        Either<ErrorResult, List<FiletypeResult>> result = await GetFiletypesScenario.ExecuteAsync(
            new GetFiletypesScenarioContext(Guid.NewGuid()));
        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r => r.Should().BeEmpty());
    }

    [Fact]
    public async Task ReturnList()
    {
        await SeedAndForgetFiletypes(10, _testOutputHelper);
        Either<ErrorResult, List<FiletypeResult>> result = await GetFiletypesScenario.ExecuteAsync(
            new GetFiletypesScenarioContext(Guid.NewGuid()));
        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r => r.Should().HaveCount(10));
    }
}