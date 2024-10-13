namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;

public class UpdateDocumentTypeScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(UpdateDocumentTypeScenarioInputInvalidData))]
    public async Task Throw_WhenInputIsInvalid(DocumentTypeInput input)
    {
        Func<Task> f = async () =>
        {
            await UpdateDocumentTypeScenario.ExecuteAsync(
                new UpdateDocumentTypeScenarioContext { Payload = input });
        };

        await f.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [ClassData(typeof(UpdateDocumentTypeScenarioInputValidData))]
    public async Task Create_WhenInputIsValid(DocumentTypeInput input)
    {
        DocumentTypeInput init = new DocumentTypeInput { Name = "init name", Description = "init desc" };
        DocumentTypeResult initResult = await AddDocumentTypeScenario.ExecuteAsync(
                new AddDocumentTypeScenarioContext { Payload = init })
            .IfNoneAsync(new DocumentTypeResult());

        input.Id = initResult.Id;
        DocumentTypeResult result = await UpdateDocumentTypeScenario.ExecuteAsync(
                new UpdateDocumentTypeScenarioContext { Payload = input })
            .IfNoneAsync(new DocumentTypeResult());

        result.Id.Should().BeGreaterThanOrEqualTo(1);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);
    }
}