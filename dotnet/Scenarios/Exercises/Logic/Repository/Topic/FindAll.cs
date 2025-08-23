namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Topic;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class TopicRepository
{
    public Either<EgError, List<TopicEntity>> GetAll(
        ExercisesContext ctx
    )
    {
        try
        {
            List<TopicEntity> result = ctx.Topics
                .Include(topic => topic.Books)
                .ThenInclude(book => book.Chapters)
                .ThenInclude(chapter => chapter.Sections)
                .ThenInclude(section => section.Exercises)
                .ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }

    public Either<EgError, List<TopicEntity>> FindAll(
        ExercisesContext ctx
    )
    {
        try
        {
            List<TopicEntity> result = ctx.Topics.ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}