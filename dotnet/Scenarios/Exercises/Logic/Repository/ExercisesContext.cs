namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository;

using Configuration;
using Microsoft.EntityFrameworkCore;
using Models;

public class ExercisesContext : DbContext
{
    public ExercisesContext(DbContextOptions<ExercisesContext> options) : base(options)
    {
    }

    protected ExercisesContext()
    {
    }

    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<ChapterEntity> Chapters { get; set; }
    public DbSet<SectionEntity> Sections { get; set; }
    public DbSet<ExerciseEntity> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TopicConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterConfiguration());
        modelBuilder.ApplyConfiguration(new SectionConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
    }
}