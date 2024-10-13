namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FiletypeConfiguration : IEntityTypeConfiguration<Filetype>
{
    public void Configure(EntityTypeBuilder<Filetype> builder)
    {
        builder.ToTable("filetype");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Name).HasColumnName("name");
        builder.Property(p => p.Description).HasColumnName("description");
        builder.Property(p => p.FileExtension).HasColumnName("fileextension");
    }
}