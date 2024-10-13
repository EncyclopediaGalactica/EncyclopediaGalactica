namespace DocumentDomain.Spec.Operations.Scenario.DocumentType;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using FluentAssertions;

public class GetDocumentByIdScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task ReturnTheDesiredResult()
    {
        DocumentTypeResult data = await AddDocumentTypeScenario.ExecuteAsync(
                new AddDocumentTypeScenarioContext
                {
                    Payload = new DocumentTypeInput { Name = "name", Description = "desc" }
                })
            .IfNoneAsync(new DocumentTypeResult());

        DocumentTypeResult result = await GetDocumentTypeByIdScenario.ExecuteAsync(
                new GetDocumentTypeByIdScenarioContext { Payload = data.Id })
            .IfNoneAsync(new DocumentTypeResult());

        result.Id.Should().Be(data.Id);
        result.Name.Should().Be(data.Name);
        result.Description.Should().Be(data.Description);
    }


    [Fact]
    public async Task ReturnNoneIfThereIsNoResult()
    {
        DocumentTypeResult result = await GetDocumentTypeByIdScenario.ExecuteAsync(
            new GetDocumentTypeByIdScenarioContext
            {
                Payload = 100
            }).IfNoneAsync(new DocumentTypeResult());

        result.Id.Should().Be(0);
        result.Name.Should().Be(null);
        result.Description.Should().Be(null);
    }
}