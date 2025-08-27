namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Book;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;
using static Prelude;

public partial class BookRepository
{
    public Either<EgError, Option<BookEntity>> FindByTopicIdAndReference(
        long topicId,
        string reference,
        ExercisesContext dbContext
    )
    {
        try
        {
            BookEntity? existingBook = dbContext.Books
                .FirstOrDefault(b => b.Reference == reference && b.TopicId == topicId);
            return existingBook == null ? Option<BookEntity>.None : Option<BookEntity>.Some(existingBook);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"No book found with reference {reference}"
                )
            );
        }
    }

    public Either<EgError, Option<BookEntity>> FindByReference(
        string reference,
        ExercisesContext dbContext
    )
    {
        try
        {
            BookEntity? existingBook = dbContext.Books.FirstOrDefault(b => b.Reference == reference);
            return existingBook == null ? Option<BookEntity>.None : Option<BookEntity>.Some(existingBook);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"No book found with reference {reference}"
                )
            );
        }
    }

    public Either<EgError, List<BookEntity>> FindByTopicName(
        string topicName,
        ExercisesContext ctx
    )
    {
        try
        {
            List<BookEntity> result = ctx.Books
                .Include(i => i.Topic)
                .Where(i => i.Topic.Name.Contains(topicName))
                .ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"{nameof(BookRepository)}.{nameof(FindByTopicName)} failed. {e.Message}",
                    e.StackTrace
                )
            );
        }
    }

    public Either<EgError, List<BookEntity>> GetAllBooks(
        ExercisesContext ctx
    )
    {
        try
        {
            List<BookEntity> result = ctx.Books.Include(i => i.Topic).ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"{nameof(BookRepository)}.{nameof(GetAllBooks)} failed. {e.Message}",
                    e.StackTrace
                )
            );
        }
    }
}