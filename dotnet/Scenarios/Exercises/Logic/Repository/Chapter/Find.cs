namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Chapter;

using System.Collections.Immutable;
using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class ChapterRepository
{
    public Either<EgError, Option<ChapterEntity>> FindByBookIdAndReference(
        long bookId,
        string reference,
        ExercisesContext ctx
    )
    {
        try
        {
            ChapterEntity? hit = ctx.Chapters
                .Where(c => c.BookId == bookId)
                .FirstOrDefault(c => c.Reference == reference);
            return hit == null ? None : Some(hit);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"Error happened while requesting {nameof(ChapterEntity)} " +
                    $"and {nameof(BookEntity)}.{nameof(BookEntity.Id)}: {bookId} " +
                    $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}"
                )
            );
        }
    }

    public Either<EgError, Option<ChapterEntity>> FindByReference(
        string reference,
        ExercisesContext ctx
    )
    {
        try
        {
            ChapterEntity? hit = ctx.Chapters.FirstOrDefault(c => c.Reference == reference);
            return hit == null ? None : Some(hit);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"Error happened while requesting {nameof(ChapterEntity)} " +
                    $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}"
                )
            );
        }
    }

    public Either<EgError, ImmutableList<ChapterEntity>> FindAll(ExercisesContext ctx)
    {
        try
        {
            ImmutableList<ChapterEntity> chapters = ctx.Chapters
                .Include(i => i.Book)
                .ThenInclude(ii => ii.Topic)
                .ToImmutableList();
            return Right(chapters);
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }

    public Either<EgError, ImmutableList<ChapterEntity>> FindByTitlePredicateAndBookAndTopic(
        string titlePredicate,
        ExercisesContext ctx
    )
    {
        try
        {
            ImmutableList<ChapterEntity> chapters = ctx.Chapters
                .Where(c => c.Title.Contains(titlePredicate))
                .Include(i => i.Book)
                .ThenInclude(ii => ii.Topic)
                .ToImmutableList();
            return Right(chapters);
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }
}