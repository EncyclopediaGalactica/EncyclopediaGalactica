namespace EncyclopediaGalactica.Services.Document.Ctx.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SourceFormatNodeEntityConfiguration : IEntityTypeConfiguration<SourceFormatNode>
{
    public void Configure(EntityTypeBuilder<SourceFormatNode> entityBuilder)
    {
        entityBuilder.ToTable("source_format_node");
        entityBuilder.HasKey(k => k.Id);
        entityBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
        entityBuilder.Property(p => p.Id).HasColumnName("id");
        entityBuilder.Property(p => p.Name).HasColumnName("name");
        entityBuilder.HasIndex(p => p.Name).IsUnique();
        entityBuilder.Property(p => p.IsRootNode)
            .HasColumnName("is_root_node")
            .IsRequired();
        entityBuilder.Property(p => p.ParentNodeId).HasColumnName("parent_node_id");
        entityBuilder.Property(p => p.RootNodeId)
            .HasColumnName("root_node_id")
            .IsRequired(false);

        entityBuilder
            .HasMany(p => p.ChildrenSourceFormatNodes)
            .WithOne(p => p.ParentSourceFormatNode)
            .HasForeignKey(k => k.ParentNodeId);

        entityBuilder
            .HasOne(p => p.RootNode)
            .WithMany(p => p.NodesInTheTree)
            .HasForeignKey(k => k.RootNodeId)
            .IsRequired(false);
    }
}