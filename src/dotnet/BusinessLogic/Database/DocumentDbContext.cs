namespace EncyclopediaGalactica.BusinessLogic.Database;

using Entities;
using EntityConfigurations;
using Microsoft.EntityFrameworkCore;

public class DocumentDbContext : DbContext
{
    public DocumentDbContext(DbContextOptions options) : base(options)
    {
    }

    protected DocumentDbContext()
    {
    }

    public DbSet<Document> Documents => Set<Document>();
    public DbSet<StructureNode> StructureNodes => Set<StructureNode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
            throw new ArgumentNullException(nameof(modelBuilder));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructureEntityConfiguration).Assembly);
    }
}