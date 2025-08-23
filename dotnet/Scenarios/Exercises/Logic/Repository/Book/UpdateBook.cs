namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Book;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class BookRepository
{
    public Either<EgError, BookEntity> UpdateBook(BookEntity input, ExercisesContext ctx)
    {
        try
        {
            BookEntity? target = ctx.Books.Find(input.Id);
            if (target == null)
            {
                return Left<EgError, BookEntity>(
                    new EgError($"There is no {nameof(BookEntity)} with id: {input.Id}")
                );
            }

            target.Reference = input.Reference;
            target.TopicId = input.TopicId;
            target.Authors = input.Authors;
            target.PageEnd = input.PageEnd;
            target.PageStart = input.PageStart;
            target.Title = input.Title;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Right<EgError, BookEntity>(target);
        }
        catch (Exception e)
        {
            return Left<EgError, BookEntity>(
                new EgError(
                    $"Error happened while updating {nameof(BookEntity)} with id: {input.Id}. Error: {e.Message}"
                )
            );
        }
    }
}