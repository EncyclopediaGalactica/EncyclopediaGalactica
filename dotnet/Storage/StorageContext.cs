namespace EncyclopediaGalactica.Storage;

using Entities;
using Microsoft.EntityFrameworkCore;

public class StorageContext : DbContext
{
    protected StorageContext()
    {
    }

    public StorageContext(DbContextOptions<StorageContext> options) : base(options)
    {
    }

    public DbSet<VertexEntity> Vertices { get; set; }
    public DbSet<EdgeEntity> Edges { get; set; }
    public DbSet<EdgeTypeEntity> EdgeTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VertexEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EdgeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EdgeTypeEntityConfiguration());
    }
}