namespace EncyclopediaGalactica.Services.Document.Tests.Tools;

using Contracts.Output;

public interface IDocumentDataSeeder
{
    Task SeedDocuments(int amount);

    Task SeedDocumentsWithRootStructureNode(int documentAmount, int structureAmountPerDocument);
    Task<DocumentResult> SeedDocument();
    Task<StructureNodeResult> SeedStructureNode(long documentId, int isRootNode = 0);
}