namespace EncyclopediaGalactica.Tools;

using Common.Contracts;
using DocumentDomain.Common.Scenario;
using DocumentDomain.Operations.Scenarios;
using LanguageExt;
using Microsoft.Extensions.Logging;

public class DocumentDataSeeder(
    IHaveInputAndResultSaga<DocumentResult, AddDocumentHavePayloadScenarioContext> addDocumentSaga,
    ILogger<DocumentDataSeeder> logger)
    : IDocumentDataSeeder
{
    private const string DocumentNameBase = "document_name_seed_";
    private const string DocumentDescriptionBase = "document_desc_seed_";
    private const int RandomSeedHigh = 100000;
    private const int RandomSeedLow = 0;

    private readonly Random _random = new Random();

    public async Task SeedDocuments(int amount)
    {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                await SeedDocument().ConfigureAwait(false);
            }
        }
    }

    public async Task SeedDocument()
    {
        AddDocumentHavePayloadScenarioContext ctx = new AddDocumentHavePayloadScenarioContext
        {
            Payload = new DocumentInput
            {
                Name = CreateDocumentName(),
                Description = CreateDocumentDescription()
            }
        };
        Option<DocumentResult> resultOption = await addDocumentSaga.ExecuteAsync(ctx).ConfigureAwait(false);

        resultOption.IfNone(() => { logger.LogInformation("=== failed creation ==="); });
        resultOption.IfSome(result => { logger.LogInformation("=== DocumentResult: id {}", result.Id); });
    }

    private string CreateDocumentName()
    {
        return $"{DocumentNameBase}{_random.Next(RandomSeedLow, RandomSeedHigh)}";
    }

    private string CreateDocumentDescription()
    {
        return $"{DocumentDescriptionBase}{_random.Next(RandomSeedLow, RandomSeedHigh)}";
    }
}