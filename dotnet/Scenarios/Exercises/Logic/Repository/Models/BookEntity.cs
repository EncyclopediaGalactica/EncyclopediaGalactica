namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Models;

public class BookEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Authors { get; set; } = string.Empty;
    public long PageStart { get; set; }
    public long PageEnd { get; set; }
    public string Reference { get; set; } = string.Empty;
    public long TopicId { get; set; }
    public TopicEntity? Topic { get; set; }

    public List<ChapterEntity> Chapters { get; set; } = [];
}