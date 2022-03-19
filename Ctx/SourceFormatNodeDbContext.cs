namespace EncyclopediaGalactica.SourceFormats.Ctx;

using Entities;
using Guards;
using Microsoft.EntityFrameworkCore;

public class SourceFormatNodeDbContext : DbContext
{
    public SourceFormatNodeDbContext(DbContextOptions options) : base(options)
    {
    }

    protected SourceFormatNodeDbContext()
    {
    }

    public DbSet<SourceFormatNode> SourceFormatNodes => Set<SourceFormatNode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Guard.NotNull(modelBuilder);

#pragma warning disable CA1062
        modelBuilder.Entity<SourceFormatNode>().ToTable("source_format_node");
        modelBuilder.Entity<SourceFormatNode>().HasKey(k => k.Id);
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Id).HasColumnName("id");
        modelBuilder.Entity<SourceFormatNode>().Property(p => p.Name).HasColumnName("name");
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