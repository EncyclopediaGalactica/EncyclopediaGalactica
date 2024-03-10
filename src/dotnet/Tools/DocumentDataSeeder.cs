namespace EncyclopediaGalactica.Tools;

public class DocumentDataSeeder(
    IAddDocumentCommand addDocumentCommand,
    IAddStructureNodeTreeCommand addStructureNodeTreeCommand,
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
                long resId = await SeedDocument().ConfigureAwait(false);

                for (int j = 0; j < structureAmountPerDocument; j++)
                {
                    await SeedStructureNode(resId, 1).ConfigureAwait(false);
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

    public async Task SeedStructureNode(long documentId, int isRootNode = 0)
    {
        await addStructureNodeTreeCommand.AddTreeAsync(
            new StructureNodeInput
            {
                DocumentId = documentId,
                IsRootNode = isRootNode
            }).ConfigureAwait(false);

        logger.LogInformation("Structure Node has been created");
    }

    public async Task<long> SeedDocument()
    {
        long resultId = await addDocumentCommand.AddAsync(new DocumentInput
        {
            Name = DocumentNameBase + _random.Next(RandomSeedLow, RandomSeedHigh),
            Description = DocumentDescriptionBase + _random.Next(RandomSeedLow, RandomSeedHigh)
        }).ConfigureAwait(false);

        logger.LogInformation("DocumentInput: Id: {Id}", resultId);
        return resultId;
    }
}