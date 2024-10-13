namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using LanguageExt;

public class AddFiletypeScenario : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(AddFiletypeScenarioInputValidationInvalidInputData))]
    public async Task ReturnErrorResult_WhenInputIsInvalid(FiletypeInput input)
    {
        Either<ErrorResult, FiletypeResult> result = await AddFiletypeScenario.ExecuteAsync(
            new AddFiletypeScenarioContext(Guid.NewGuid(), input));
        result.IsLeft.Should().BeTrue();
        result.IsRight.Should().BeFalse();
        result.IfLeft(er =>
        {
            er.CorrelationId.Should().NotBeEmpty();
            er.ErrorMessage.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task ReturnFiletypeResult_WhenOperationIsSuccessful()
    {
        Either<ErrorResult, FiletypeResult> result = await AddFiletypeScenario.ExecuteAsync(
            new AddFiletypeScenarioContext(Guid.NewGuid(),
                new FiletypeInput { Id = 0, Name = "asdd", Description = "asd", FileExtension = "asd" }));
        result.IsLeft.Should().BeFalse();
        result.IsRight.Should().BeTrue();
        result.IfRight(r =>
        {
            r.Id.Should().BeGreaterOrEqualTo(1);
            r.Name.Should().Be("asdd");
            r.Description.Should().Be("asd");
            r.FileExtension.Should().Be("asd");
        });
    }
}