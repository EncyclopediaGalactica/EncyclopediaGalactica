namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("document_type");

        builder.Property(p => p.Id).HasColumnName("id");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasColumnName("name");
        builder.HasIndex(p => p.Name).IsUnique();

        builder.Property(p => p.Description).HasColumnName("description");
    }
}