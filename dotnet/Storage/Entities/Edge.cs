namespace EncyclopediaGalactica.Storage.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EdgeEntity
{
    public long Id { get; set; }
    public long FromVertexId { get; set; }
    public VertexEntity FromVertex { get; set; }
    public long ToVertexId { get; set; }
    public VertexEntity ToVertex { get; set; }
    public long EdgeTypeId { get; set; }
    public EdgeTypeEntity EdgeType { get; set; }
}

public class EdgeEntityConfiguration : IEntityTypeConfiguration<EdgeEntity>
{
    public void Configure(EntityTypeBuilder<EdgeEntity> builder)
    {
        builder.ToTable("edges");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.FromVertexId).HasColumnName("from_vertex_id");
        builder.Property(e => e.ToVertexId).HasColumnName("to_vertex_id");
        builder.Property(e => e.EdgeTypeId).HasColumnName("edge_type_id");
    }
}