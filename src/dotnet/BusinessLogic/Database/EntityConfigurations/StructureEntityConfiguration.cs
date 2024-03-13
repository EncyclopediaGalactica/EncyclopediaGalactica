namespace EncyclopediaGalactica.BusinessLogic.Database.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StructureEntityConfiguration : IEntityTypeConfiguration<StructureNode>
{
    public void Configure(EntityTypeBuilder<StructureNode> builder)
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