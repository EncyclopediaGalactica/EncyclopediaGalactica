namespace EncyclopediaGalactica.Tools;

public interface IDocumentDataSeeder
{
    Task SeedDocuments(int amount);

    Task SeedDocumentsWithRootStructureNode(int documentAmount, int structureAmountPerDocument);
    Task<long> SeedDocument();
    Task SeedStructureNode(long documentId, int isRootNode = 0);
}