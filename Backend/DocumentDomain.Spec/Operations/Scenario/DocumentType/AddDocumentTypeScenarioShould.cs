namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;
using LanguageExt;

public class AddDocumentTypeScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(AddDocumentTypeScenarioInputInvalidData))]
    public async Task Throw_WhenInputIsInvalid(DocumentTypeInput input)
    {
        Func<Task> f = async () =>
        {
            await AddDocumentTypeScenario.ExecuteAsync(
                new AddDocumentTypeScenarioContext { Payload = input }
            );
        };
        await f.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [ClassData(typeof(AddDocumentTypeScenarioInputValidData))]
    public async Task Create_WhenInputIsValid(DocumentTypeInput input)
    {
        Option<DocumentTypeResult> result = await AddDocumentTypeScenario.ExecuteAsync(
            new AddDocumentTypeScenarioContext { Payload = input });

        result.IsSome.Should().BeTrue();

        result.IfNone(new DocumentTypeResult()).Id.Should().BeGreaterThanOrEqualTo(1);
        result.IfNone(new DocumentTypeResult()).Name.Should().Be(input.Name);
        result.IfNone(new DocumentTypeResult()).Description.Should().Be(input.Description);
    }
}