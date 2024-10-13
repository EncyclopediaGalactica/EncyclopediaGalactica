namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DocumentStructureNodeConfiguration : IEntityTypeConfiguration<DocumentStructureNode>
{
    public void Configure(EntityTypeBuilder<DocumentStructureNode> builder)
    {
        builder.ToTable("structure_node");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");

        builder.Property(p => p.DocumentId).HasColumnName("document_id");

        builder.Property(p => p.IsRootNode).HasColumnName("is_root_node");
        builder.Property(p => p.IsRootNode).HasDefaultValue(0);
    }
}