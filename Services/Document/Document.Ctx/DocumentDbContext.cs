namespace EncyclopediaGalactica.Services.Document.Ctx;

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

    public DbSet<SourceFormatNode> SourceFormatNodes => Set<SourceFormatNode>();
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
            throw new ArgumentNullException(nameof(modelBuilder));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructureEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SourceFormatNodeEntityConfiguration).Assembly);
    }
}