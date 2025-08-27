namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Book;

using Common;
using Models;
using static Prelude;

public partial class BookRepository
{
    public Either<EgError, BookEntity> AddNewBookEntity(
        BookEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Books.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            return Left(new EgError($"{e.Message}"));
        }
    }
}