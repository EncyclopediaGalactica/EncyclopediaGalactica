namespace EncyclopediaGalactica.SourceFormats.Ctx;

using Entities;
using Microsoft.EntityFrameworkCore;

public class SourceFormatsDbContext : DbContext
{
    public SourceFormatsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected SourceFormatsDbContext()
    {
    }

    public DbSet<SourceFormatNode> SourceFormatNodes => Set<SourceFormatNode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
            throw new ArgumentNullException(nameof(modelBuilder));

#pragma warning disable CA1062
        modelBuilder.Entity<SourceFormatNode>().ToTable("source_format_node");
        modelBuilder.Entity<SourceFormatNode>().HasKey(k => k.Id);
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Id).HasColumnName("id");
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Name).HasColumnName("name");
        modelBuilder.Entity<SourceFormatNode>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.IsRootNode).HasColumnName("is_root_node");
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.IsRootNode).IsRequired();
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.ParentNodeId).HasColumnName("parent_node_id");
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.RootNodeId).HasColumnName("root_node_id");

        modelBuilder.Entity<SourceFormatNode>()
            .HasMany(p => p.ChildrenSourceFormatNodes)
            .WithOne(p => p.ParentSourceFormatNode)
            .HasForeignKey(k => k.ParentNodeId);

#pragma warning restore CA1062
    }
}