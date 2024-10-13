namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("application");
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedOnAdd();
        builder.Property(k => k.Id).HasColumnName("id");
        builder.Property(k => k.Name).HasColumnName("name");
        builder.Property(k => k.Description).HasColumnName("description");
    }
}