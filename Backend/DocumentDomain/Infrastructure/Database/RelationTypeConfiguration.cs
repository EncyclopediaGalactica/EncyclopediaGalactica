namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RelationTypeConfiguration : IEntityTypeConfiguration<RelationType>
{
    public void Configure(EntityTypeBuilder<RelationType> builder)
    {
        builder.ToTable("relation_type");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Name).HasColumnName("name");
        builder.Property(p => p.Description).HasColumnName("description");
    }
}