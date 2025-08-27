namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.CatalogParser.Model;

using Common;
using Repository.Models;
using static Prelude;

public class Topic
{
    public string? Name { get; set; }

    public string? Reference { get; set; }

    public List<Book> Books { get; set; } = [];
}

public static class TopicExtensions
{
    public static Either<EgError, TopicEntity> ToTopicEntity(
        this Topic topic
    )
    {
        try
        {
            return Right<EgError, TopicEntity>(
                new TopicEntity { Id = 0, Name = topic.Name, Reference = topic.Reference, }
            );
        }
        catch (Exception e)
        {
            return Left<EgError, TopicEntity>(
                new EgError($"Mapping of {nameof(TopicEntity)} failed. Error: {e.Message}")
            );
        }
    }
}