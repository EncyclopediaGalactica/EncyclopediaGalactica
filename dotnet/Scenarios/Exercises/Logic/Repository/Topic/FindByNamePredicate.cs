namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Topic;

using System.Collections.Immutable;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using static Prelude;

public partial class TopicRepository
{
    public Either<EgError, ImmutableList<TopicEntity>> FindAllWithBooksByNamePredicate(
        string namePredicate,
        ExercisesContext ctx
    )
    {
        try
        {
            logger.LogDebug(
                "{MethodName} Input name predicate: {Predicate}",
                nameof(FindAllWithBooksByNamePredicate),
                namePredicate
            );

            ImmutableList<TopicEntity> result = ctx.Topics
                .Include(i => i.Books)
                .Where(t => t.Name.Contains(namePredicate))
                .ToImmutableList();

            logger.LogDebug(
                "{MethodName}: Result count: {Count}",
                nameof(FindAllWithBooksByNamePredicate),
                result.Count
            );

            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"Error happened while looking for {nameof(TopicEntity)} with name: {namePredicate}. Error: {e.Message}"
                )
            );
        }
    }
}