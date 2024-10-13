namespace Host.Seeders;

using EncyclopediaGalactica.DocumentDomain.Entity;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

public class DocumentTypeSeeder
{
    public static void SeedN(int amount, DocumentDomainDbContext ctx)
    {
        for (int i = 0; i <= amount; i++)
        {
            ctx.DocumentTypes.Add(
                new DocumentType
                {
                    Name = $"seeded name {i}",
                    Description = $"seeded description {i}"
                });
            ctx.SaveChanges();
        }

        Console.WriteLine($"=== Data Seeder: {amount} {nameof(DocumentType)} was created.");
    }
}