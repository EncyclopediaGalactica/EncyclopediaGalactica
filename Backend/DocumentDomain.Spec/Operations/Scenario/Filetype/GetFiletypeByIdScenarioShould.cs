namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using LanguageExt;

public class GetFiletypeByIdScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnErrorResult_WhenInputIdInvalid()
    {
        Either<ErrorResult, FiletypeResult> result = await GetFiletypeByIdScenario.ExecuteAsync(
            new GetFiletypeByIdScenarioContext(Guid.NewGuid(), new FiletypeInput { Id = 0 }));
        result.IsLeft.Should().BeTrue();
        result.IsRight.Should().BeFalse();
        result.IfLeft(e =>
        {
            e.CorrelationId.Should().NotBeEmpty();
            e.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnErrorResult_WhenThereIsNoSuchEntity()
    {
        Either<ErrorResult, FiletypeResult> result = await GetFiletypeByIdScenario.ExecuteAsync(
            new GetFiletypeByIdScenarioContext(Guid.NewGuid(), new FiletypeInput { Id = 190 }));
        result.IsLeft.Should().BeTrue();
        result.IsRight.Should().BeFalse();
        result.IfLeft(e =>
        {
            e.CorrelationId.Should().NotBeEmpty();
            e.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnFiletypeResult_WhenOperationIsSuccessful()
    {
        FiletypeInput d = new FiletypeInput { Id = 0, Name = "asd", Description = "asd", FileExtension = "asd" };
        Either<ErrorResult, FiletypeResult> data = await AddFiletypeScenario.ExecuteAsync(
            new AddFiletypeScenarioContext(Guid.NewGuid(), d));
        data.IsLeft.Should().BeFalse();
        data.IsRight.Should().BeTrue();

        FiletypeInput query = null;
        data.IfRight(d => query = new FiletypeInput { Id = d.Id, Name = d.Name, Description = d.Description, FileExtension = d.FileExtension });
        Either<ErrorResult, FiletypeResult> result = await GetFiletypeByIdScenario.ExecuteAsync(
            new GetFiletypeByIdScenarioContext(Guid.NewGuid(), query!));

        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(res =>
        {
            res.Id.Should().Be(query!.Id);
            res.Name.Should().Be(query!.Name);
            res.Description.Should().Be(query!.Description);
            res.FileExtension.Should().Be(query!.FileExtension);
        });
    }
}