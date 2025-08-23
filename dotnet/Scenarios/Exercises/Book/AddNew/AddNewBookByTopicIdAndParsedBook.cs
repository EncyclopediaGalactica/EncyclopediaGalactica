namespace EncyclopediaGalactica.Scenarios.Exercises.Book.AddNew;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Models;
using static Prelude;

public class AddNewBookByTopicIdAndParsedBook(
    BookRepository bookRepository,
    AddNewBookScenarioInputValidator validator
)
{
    public Either<EgError, BookEntity> Execute(
        long topicId,
        Book parsedBook,
        ExercisesContext dbContext
    ) =>
        from mappedInput in MapToBookEntity(parsedBook, topicId)
        from validatedInput in ValidateInputEntity(mappedInput)
        from recordedEntity in SaveNewEntity(validatedInput, dbContext)
        select recordedEntity;

    private Either<EgError, BookEntity> MapToBookEntity(
        Book parsedBook,
        long topicId
    ) =>
        parsedBook.ToBookEntity().Match(
            val =>
            {
                val.TopicId = topicId;
                return Right<EgError, BookEntity>(val);
            },
            () => Left<EgError, BookEntity>(new EgError("Failed mapping"))
        );

    private Either<EgError, BookEntity> SaveNewEntity(
        BookEntity validatedInput,
        ExercisesContext context
    ) =>
        from result in bookRepository.AddNewBookEntity(validatedInput, context)
        select result;

    private Either<EgError, BookEntity> ValidateInputEntity(
        BookEntity mappedInput
    ) =>
        validator.IsValid(mappedInput);
}