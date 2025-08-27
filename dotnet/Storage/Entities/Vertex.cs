namespace EncyclopediaGalactica.Storage.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VertexEntity
{
    public long Id { get; set; }
    public Dictionary<string, object> Data { get; set; }
    
}

public class VertexEntityConfiguration : IEntityTypeConfiguration<VertexEntity>
{
    public void Configure(EntityTypeBuilder<VertexEntity> builder)
    {
        builder.ToTable("vertices");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.Data).HasColumnName("data");
        builder.Property(x => x.Data).HasColumnType("jsonb");

    }
}