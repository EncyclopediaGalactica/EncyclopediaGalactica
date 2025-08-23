namespace EncyclopediaGalactica.Scenarios.Exercises.Book.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Models;

public class FindBookByTopicIdAndReferenceScenario(
    BookRepository bookRepository)
{
    public Either<EgError, Option<BookEntity>> Execute(
        long topicId,
        string reference,
        ExercisesContext dbContext
    ) =>
        from r in bookRepository.FindByTopicIdAndReference(topicId, reference, dbContext)
        select r;
}