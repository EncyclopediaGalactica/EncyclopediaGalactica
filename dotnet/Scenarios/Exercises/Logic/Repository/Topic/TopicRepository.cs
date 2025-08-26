namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Topic;

using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using static Prelude;

public partial class TopicRepository(
    DbContextOptions<ExercisesContext> dbContextOptions,
    ILogger<TopicRepository> logger
)
{
    public async Task<List<TopicEntity>> GetEverything()
    {
        await using ExercisesContext ctx = new(dbContextOptions);
        return await ctx.Topics
            .Include(topic => topic.Books)
            .ThenInclude(book => book.Chapters)
            .ThenInclude(chapter => chapter.Sections)
            .ThenInclude(section => section.Exercises)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public Either<EgError, TopicEntity> AddNewTopic(
        TopicEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Topics.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in repository: {e.Message}");
            return Left(
                new EgError($"Error happened while creating a new {nameof(TopicEntity)}. Error: {e.Message}")
            );
        }
    }
}