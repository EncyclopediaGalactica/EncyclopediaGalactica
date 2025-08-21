namespace EncyclopediaGalactica.Storage.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EdgeTypeEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Reference { get; set; }
}

public class EdgeTypeEntityConfiguration : IEntityTypeConfiguration<EdgeTypeEntity>
{
    public void Configure(EntityTypeBuilder<EdgeTypeEntity> builder)
    {
        builder.ToTable("edge_type");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.Reference).HasColumnName("reference");
    }
}