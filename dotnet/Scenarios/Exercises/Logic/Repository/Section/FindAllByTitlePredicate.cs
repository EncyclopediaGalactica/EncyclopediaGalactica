namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Section;

using System.Collections.Immutable;
using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class SectionRepository
{
    public Either<EgError, ImmutableList<SectionEntity>> FindAllByTitlePredicateAndChapterAndBookAndTopic(
        string titlePredicate,
        ExercisesContext ctx
    )
    {
        try
        {
            ImmutableList<SectionEntity> result = ctx.Sections
                .Include(i => i.Chapter)
                .ThenInclude(i => i.Book)
                .ThenInclude(i => i.Topic)
                .Where(p => p.Title.Contains(titlePredicate))
                .ToImmutableList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}