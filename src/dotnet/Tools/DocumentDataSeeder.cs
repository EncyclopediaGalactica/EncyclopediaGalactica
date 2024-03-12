namespace EncyclopediaGalactica.Tools;

using BusinessLogic.Contracts;
using BusinessLogic.Sagas.Document;
using BusinessLogic.Sagas.Interfaces;
using Microsoft.Extensions.Logging;

public class DocumentDataSeeder(
    IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext> addDocumentSaga,
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

    public async Task<DocumentResult> SeedDocument()
    {
        AddDocumentSagaContext ctx = new AddDocumentSagaContext
        {
            Payload = new DocumentInput
            {
                Name = CreateDocumentName(),
                Description = CreateDocumentDescription()
            }
        };
        DocumentResult result = await addDocumentSaga.ExecuteAsync(ctx).ConfigureAwait(false);

        logger.LogInformation("DocumentInput: Id: {Id}", result);
        return result;
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