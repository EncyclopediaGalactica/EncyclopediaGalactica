namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using LanguageExt;

public class EditFiletypeScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(EditFiletypeScenarioInputValidatorInvalidInputData))]
    public async Task ReturnErrorResult_WhenOperationFails(FiletypeInput input)
    {
        Either<ErrorResult, FiletypeResult> result = await EditFiletypeScenario.ExecuteAsync(
            new EditFiletypeScenarioContext(Guid.NewGuid(), input));

        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(er =>
        {
            er.CorrelationId.Should().NotBeEmpty();
            er.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnErrorResult_WhenThereIsNoSuchEntity()
    {
        Either<ErrorResult, FiletypeResult> result = await EditFiletypeScenario.ExecuteAsync(
            new EditFiletypeScenarioContext(Guid.NewGuid(), new FiletypeInput { Id = 1000 }));

        result.IsRight.Should().BeFalse();
        result.IsLeft.Should().BeTrue();
        result.IfLeft(er =>
        {
            er.CorrelationId.Should().NotBeEmpty();
            er.ErrorMessage.Should().NotBeEmpty();
        });
    }

    public async Task ReturnFiletypeResult_WhenOperationIsSuccessful()
    {
    }
}