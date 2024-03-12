namespace EncyclopediaGalactica.Tools;

using BusinessLogic.Contracts;

public interface IDocumentDataSeeder
{
    Task SeedDocuments(int amount);

    Task<DocumentResult> SeedDocument();
}