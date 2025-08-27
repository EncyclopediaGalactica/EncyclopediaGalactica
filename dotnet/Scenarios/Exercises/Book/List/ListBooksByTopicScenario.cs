namespace EncyclopediaGalactica.Scenarios.Exercises.Book.List;

using Common;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Models;
using Microsoft.Extensions.Logging;

public class ListBooksByTopicScenario(
    ExercisesContext ctx,
    BookRepository bookRepository,
    ILogger<ListBooksByTopicScenario> logger
)
{
    public Either<EgError, List<ListBooksByTopicScenarioResult>> Execute(
        ListBooksByTopicScenarioInput input
    )
    {
        Option<string> topicNameOption = input.TopicName == string.Empty ? None : Some(input.TopicName);
        return from result in PopulateBooksList(topicNameOption)
            from mappedResult in result.ToListBooksByTopicScenarioResultList()
            select mappedResult;
    }

    private Either<EgError, List<BookEntity>> PopulateBooksList(Option<string> topicNameOption)
    {
        if (topicNameOption.IsNone)
        {
            Either<EgError, List<BookEntity>> r = bookRepository.GetAllBooks(ctx);
            r.IfRight(res => { logger.LogDebug($"Result count: {res.Count}"); });
            r.IfLeft(res => { logger.LogError($"Error: {res.Message}"); });
            return r;
        }

        string t = string.Empty;
        topicNameOption.IfSome(s => t = s);
        return bookRepository.FindByTopicName(t!, ctx);
    }
}

public record ListBooksByTopicScenarioResult()
{
    public long BookId { get; init; }
    public string BookTitle { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string BookReference { get; init; } = string.Empty;
    public string? TopicName { get; init; } = string.Empty;
};

public record ListBooksByTopicScenarioInput()
{
    public string TopicName { get; init; } = string.Empty;
}

public static class ListBooksByTopicScenarioExtensions
{
    public static Either<EgError, List<ListBooksByTopicScenarioResult>> ToListBooksByTopicScenarioResultList(
        this List<BookEntity> books
    ) =>
        toSeq(books)
            .Traverse(i => i.ToListBooksByTopicScenarioResult())
            .Map(items => items.ToList())
            .As();

    public static Either<EgError, ListBooksByTopicScenarioResult> ToListBooksByTopicScenarioResult(
        this BookEntity bookEntity
    )
    {
        try
        {
            return Right(
                new ListBooksByTopicScenarioResult()
                {
                    BookId = bookEntity.Id,
                    BookTitle = bookEntity.Title,
                    Author = bookEntity.Authors,
                    BookReference = bookEntity.Reference,
                    TopicName = bookEntity.Topic?.Name,
                }
            );
        }
        catch (Exception ex)
        {
            return Left(
                new EgError(
                    $"{nameof(ListBooksByTopicScenarioExtensions)}.{nameof(ToListBooksByTopicScenarioResult)}: {ex.Message}",
                    ex.StackTrace
                )
            );
        }
    }
}