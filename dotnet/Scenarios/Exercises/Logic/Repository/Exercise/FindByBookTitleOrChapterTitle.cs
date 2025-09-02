namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Exercise;

using System.Linq.Expressions;
using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class ExerciseRepository
{
    public Either<EgError, List<ExerciseEntity>> FindByBookTitleOrChapterTitle(
        FindByBookTitleOrChapterTitleInput input,
        ExercisesContext ctx
    )
    {
        try
        {
            Expression<Func<ExerciseEntity, bool>> predicate = FindByBookTitleOrChapterTitlePredicateBuilder(input);
            List<ExerciseEntity> target = ctx.Exercises
                .Include(i => i.Book)
                .Include(i => i.Chapter)
                .Where(predicate)
                .ToList();
            return Right(target);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }

    private Expression<Func<ExerciseEntity, bool>> FindByBookTitleOrChapterTitlePredicateBuilder(
        FindByBookTitleOrChapterTitleInput input
    )
    {
        Expression<Func<ExerciseEntity, bool>> predicate =
            ExerciseFindByBookTitleOrChapterTitleFilterExtensions.True<ExerciseEntity>();
        if (string.IsNullOrEmpty(input.BookTitleFilter))
        {
            predicate = predicate.Or(pred => pred.Book.Title.Contains(input.BookTitleFilter!));
        }

        if (string.IsNullOrEmpty(input.ChapterTitleFilter))
        {
            predicate = predicate.Or(pred => pred.Chapter.Title.Contains(input.ChapterTitleFilter!));
        }

        return predicate;
    }
}

public static class ExerciseFindByBookTitleOrChapterTitleFilterExtensions
{
    public static Expression<Func<T, bool>> True<T>() => _ => true;

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second
    )
    {
        InvocationExpression invokedExpression = Expression.Invoke(second, first.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, invokedExpression), first.Parameters);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second
    )
    {
        InvocationExpression invokedExpression = Expression.Invoke(second, first.Parameters);
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(first.Body, invokedExpression), first.Parameters);
    }
}

public record FindByBookTitleOrChapterTitleInput(
    string BookTitlePattern,
    string ChapterTitlePattern)
{
    public string? BookTitleFilter { get; }
    public string? ChapterTitleFilter { get; }
}