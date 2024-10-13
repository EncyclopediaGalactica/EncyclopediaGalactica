namespace DocumentDomain.Spec.Operations.Scenario;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Common.Scenario;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;
using FluentAssertions;

public class AddDocumentScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task AddDocument()
    {
        DocumentInput input = new DocumentInput
        {
            Name = "name",
            Description = "description"
        };
        AddDocumentHavePayloadScenarioContext ctx = new AddDocumentHavePayloadScenarioContext
        {
            Payload = input
        };
        DocumentResult result = await AddDocumentSaga.ExecuteAsync(ctx).IfNoneAsync(new DocumentResult());

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThanOrEqualTo(1);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);
    }

    [Theory]
    [ClassData(typeof(AddDocumentSagaInputInvalidData))]
    public async Task ThrowWhenInputIsInvalid(DocumentInput documentInput)
    {
        AddDocumentHavePayloadScenarioContext ctx = new AddDocumentHavePayloadScenarioContext
        {
            Payload = documentInput
        };
        Func<Task> task = async () => { await AddDocumentSaga.ExecuteAsync(ctx); };
        await task.Should().ThrowExactlyAsync<SagaException>();
    }
}