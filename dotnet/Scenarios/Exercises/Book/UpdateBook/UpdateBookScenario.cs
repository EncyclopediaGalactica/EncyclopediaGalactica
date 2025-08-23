namespace EncyclopediaGalactica.Scenarios.Exercises.Book.UpdateBook;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Models;
using static Prelude;

public class UpdateBookScenario(
    UpdateBookScenarioInputValidator validator,
    BookRepository bookRepository
)
{
    public Either<EgError, BookEntity> Execute(long bookId, Book parsedBook, ExercisesContext ctx) =>
        from mappedInput in MapInputToEntity(parsedBook, bookId)
        from validatedInput in ValidateInputEntity(mappedInput)
        from updatedBookEntity in UpdateBookEntity(validatedInput, ctx)
        select updatedBookEntity;

    private Either<EgError, BookEntity> UpdateBookEntity(BookEntity updated, ExercisesContext ctx) =>
        bookRepository.UpdateBook(updated, ctx);


    private Either<EgError, BookEntity> MapInputToEntity(Book parsedBook, long bookId) =>
        parsedBook.ToBookEntity().Match(
            result =>
            {
                result.Id = bookId;
                return Right<EgError, BookEntity>(result);
            },
            () => Left<EgError, BookEntity>(new EgError("Mapping has failed."))
        );

    private Either<EgError, BookEntity> ValidateInputEntity(BookEntity inputEntity) =>
        validator.IsValid(inputEntity);
}