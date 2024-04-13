namespace EncyclopediaGalactica.BusinessLogic.Database.EntityConfigurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RelationEntityConfiguration : IEntityTypeConfiguration<Relation>
{
    public void Configure(EntityTypeBuilder<Relation> builder)
    {
        builder.ToTable("relation");
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.LeftEndStructureNodeId).HasColumnName("left_end_id");
        builder.Property(p => p.RightEndStructureNodeId).HasColumnName("right_end_id");
    }
}