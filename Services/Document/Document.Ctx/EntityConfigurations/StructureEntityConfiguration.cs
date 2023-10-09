namespace EncyclopediaGalactica.Services.Document.Ctx.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StructureEntityConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.ToTable("structure");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.ParentId).HasColumnName("parent_id");
    }
}