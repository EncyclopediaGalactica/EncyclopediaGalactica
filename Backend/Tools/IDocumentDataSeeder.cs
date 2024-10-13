namespace EncyclopediaGalactica.Tools;

public interface IDocumentDataSeeder
{
    Task SeedDocuments(int amount);

    Task SeedDocument();
}