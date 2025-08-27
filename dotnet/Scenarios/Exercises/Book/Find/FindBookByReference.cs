namespace EncyclopediaGalactica.Scenarios.Exercises.Book.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Models;

public class FindBookByReferenceScenario(
    BookRepository bookRepository)
{
    public Either<EgError, Option<BookEntity>> Execute(
        string reference,
        ExercisesContext dbContext
    ) =>
        from r in bookRepository.FindByReference(reference, dbContext)
        select r;
}