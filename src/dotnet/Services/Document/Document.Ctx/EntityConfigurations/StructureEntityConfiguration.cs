namespace EncyclopediaGalactica.Services.Document.Ctx.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StructureEntityConfiguration : IEntityTypeConfiguration<StructureNode>
{
    public void Configure(EntityTypeBuilder<StructureNode> builder)
    {
        builder.ToTable("structure");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
    }
}