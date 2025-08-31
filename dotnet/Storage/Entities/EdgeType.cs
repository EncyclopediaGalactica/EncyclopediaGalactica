namespace EncyclopediaGalactica.Storage.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EdgeTypeEntity
{
    public long Id { get; set; }
    public IEnumerable<VertexEntity> Vertices { get; } = [];
    public IEnumerable<VertexEntity>? EdgeTypeVertices { get; } = [];
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
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

        builder
            .HasMany(e => e.Vertices)
            .WithMany(e => e.EdgeTypeVertices)
            .UsingEntity<EdgeTypeVertices>("edgetype_vertices");
    }
}