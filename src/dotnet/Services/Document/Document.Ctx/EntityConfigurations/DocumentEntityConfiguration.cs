namespace EncyclopediaGalactica.Services.Document.Ctx.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("document");
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedOnAdd();
        builder.Property(k => k.Id).HasColumnName("id");
        builder.Property(k => k.Name).HasColumnName("name");
        builder.HasIndex(k => k.Name).IsUnique();
        builder.Property(k => k.Description).HasColumnName("description");
        builder.Property(k => k.Uri).HasColumnName("uri");
    }
}