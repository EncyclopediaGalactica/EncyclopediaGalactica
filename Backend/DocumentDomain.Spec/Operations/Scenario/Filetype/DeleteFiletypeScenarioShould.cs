namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using LanguageExt;

public class DeleteFiletypeScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnErrorResult_WhenInputIsInvalid()
    {
        Either<ErrorResult, FiletypeResult> result = await DeleteFiletypeScenario.ExecuteAsync(
            new DeleteFiletypeScenarioContext(Guid.NewGuid(),
                new FiletypeInput { Id = 0 }));
        result.IsLeft.Should().BeTrue();
        result.IsRight.Should().BeFalse();
        result.IfLeft(er =>
        {
            er.CorrelationId.Should().NotBeEmpty();
            er.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnErrorResult_WhenNoSuchEntity()
    {
        Either<ErrorResult, FiletypeResult> result = await DeleteFiletypeScenario.ExecuteAsync(
            new DeleteFiletypeScenarioContext(Guid.NewGuid(),
                new FiletypeInput { Id = 190 }));
        result.IsLeft.Should().BeTrue();
        result.IsRight.Should().BeFalse();
        result.IfLeft(er =>
        {
            er.CorrelationId.Should().NotBeEmpty();
            er.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnFiletypeResult_WhenTheOperationIsSuccessful()
    {
        FiletypeInput delete = null;
        Either<ErrorResult, FiletypeResult> data = await AddFiletypeScenario.ExecuteAsync(
            new AddFiletypeScenarioContext(Guid.NewGuid(), new FiletypeInput
            {
                Id = 0, Name = "asd", Description = "asd", FileExtension = "asd"
            }));
        data.IsRight.Should().BeTrue();
        data.IsLeft.Should().BeFalse();
        data.IfRight(res => delete = new FiletypeInput { Id = res.Id });

        Either<ErrorResult, FiletypeResult> result = await DeleteFiletypeScenario.ExecuteAsync(
            new DeleteFiletypeScenarioContext(Guid.NewGuid(), delete!));

        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r => { r.Id.Should().Be(0); });
    }
}