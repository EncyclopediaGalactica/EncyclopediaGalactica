namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;
using LanguageExt;

public class GetDocumentTypesScenarioShould : ScenarioBaseTest
{
    [Theory]
    [ClassData(typeof(GetDocumentTypesScenarioData))]
    public async Task ListAllItems(List<DocumentTypeInput> testData)
    {
        testData.ForEach(async i =>
        {
            await AddDocumentTypeScenario.ExecuteAsync(
                new AddDocumentTypeScenarioContext { Payload = i });
        });


        Option<List<DocumentTypeResult>> result = await GetDocumentTypesScenario.ExecuteAsync(
            new GetDocumentTypesScenarioContext());

        result.IfSome((r) =>
        {
            testData.ForEach(i =>
            {
                r.First(w => w.Name == i.Name && w.Description == i.Description && w.Id != 0)
                    .Should().NotBeNull();
            });
        });
    }
}