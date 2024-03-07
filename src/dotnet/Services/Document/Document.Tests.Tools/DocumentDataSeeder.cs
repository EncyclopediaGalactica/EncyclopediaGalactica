namespace EncyclopediaGalactica.Services.Document.Tests.Tools;

using Contracts.Input;
using Contracts.Output;
using Microsoft.Extensions.Logging;
using Scenario.Interfaces.Document;
using Scenario.Interfaces.StructureNode;

public class DocumentDataSeeder(
    IAddDocumentScenario addDocumentScenario,
    IAddNewRootStructureNodeScenario addNewRootStructureNodeScenario,
    ILogger<DocumentDataSeeder> logger)
    : IDocumentDataSeeder
{
    private const string DocumentNameBase = "document_name_seed_";
    private const string DocumentDescriptionBase = "document_desc_seed_";
    private const int RandomSeedHigh = 100000;
    private const int RandomSeedLow = 0;

    private readonly Random _random = new Random();

    public async Task SeedDocumentsWithRootStructureNode(int documentAmount, int structureAmountPerDocument)
    {
        if (documentAmount > 0)
        {
            for (int i = 0; i < documentAmount; i++)
            {
                DocumentResult res = await SeedDocument().ConfigureAwait(false);

                for (int j = 0; j < structureAmountPerDocument; j++)
                {
                    await SeedStructureNode(res.Id, 1).ConfigureAwait(false);
                }
            }
        }
    }

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
        DocumentResult result = await addDocumentScenario.AddAsync(new DocumentInput
        {
            Name = DocumentNameBase + _random.Next(RandomSeedLow, RandomSeedHigh),
            Description = DocumentDescriptionBase + _random.Next(RandomSeedLow, RandomSeedHigh)
        }).ConfigureAwait(false);

        logger.LogInformation("DocumentInput: Id: {Id}", result.Id);
        return result;
    }

    public async Task<StructureNodeResult> SeedStructureNode(long documentId, int isRootNode = 0)
    {
        StructureNodeResult result = await addNewRootStructureNodeScenario.AddNewRootNodeAsync(
            new StructureNodeInput
            {
                DocumentId = documentId,
                IsRootNode = isRootNode
            }).ConfigureAwait(false);

        logger.LogInformation("Structure Node: Id: {Id}", result.Id);

        return result;
    }
}