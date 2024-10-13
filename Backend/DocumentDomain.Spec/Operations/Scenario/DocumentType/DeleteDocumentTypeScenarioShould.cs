namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;
using LanguageExt;

public class DeleteDocumentTypeScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(DeleteDocumentTypeScenarioInputInvalidData))]
    public async Task Throw_WhenInputIsInvalid(DocumentTypeInput input)
    {
        Func<Task> f = async () =>
        {
            await DeleteDocumentTypeScenario.ExecuteAsync(
                new DeleteDocumentTypeHavePayloadScenarioContext { Payload = input });
        };
        await f.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Delete_WhenInputIsValid()
    {
        DocumentTypeInput tbd = new DocumentTypeInput { Name = "tbd name", Description = "tbd desc" };
        DocumentTypeResult res = await AddDocumentTypeScenario.ExecuteAsync(
                new AddDocumentTypeScenarioContext { Payload = tbd })
            .IfNoneAsync(new DocumentTypeResult());

        DocumentTypeInput delete = new DocumentTypeInput { Id = res.Id };
        await DeleteDocumentTypeScenario.ExecuteAsync(
            new DeleteDocumentTypeHavePayloadScenarioContext { Payload = delete });

        Option<List<DocumentTypeResult>> result = await GetDocumentTypesScenario.ExecuteAsync(
            new GetDocumentTypesScenarioContext());
        result.IfSome(r => { r.Where(p => p.Id == res.Id).ToList().Count.Should().Be(0); });
        result.IfNone(() => { true.Should().BeFalse(); });
    }
}