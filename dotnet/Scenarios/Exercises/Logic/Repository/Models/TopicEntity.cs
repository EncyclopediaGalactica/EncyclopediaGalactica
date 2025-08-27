namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Models;

public class TopicEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public List<BookEntity> Books { get; set; } = [];
}