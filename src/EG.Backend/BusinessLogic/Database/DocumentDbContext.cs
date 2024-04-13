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
    public DbSet<Relation> Relations => Set<Relation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
            throw new ArgumentNullException(nameof(modelBuilder));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructureEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RelationEntityConfiguration).Assembly);
    }
}