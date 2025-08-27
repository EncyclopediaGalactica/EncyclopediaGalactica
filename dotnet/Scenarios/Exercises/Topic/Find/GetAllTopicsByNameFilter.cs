namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Find;

using System.Collections.Immutable;
using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Topic;

public class FindAllTopicsByNamePredicateScenario(
    TopicRepository repository
)
{
    public Either<EgError, ImmutableList<FindAllTopicsByNamePredicateScenarioResult>> Execute(
        FindAllTopicsByNamePredicateScenarioInput input,
        ExercisesContext ctx
    ) =>
        from all in GetTopicEntities(input.NamePredicate, ctx)
        from mappedResult in all.ToFindAllTopicsByNamePredicateScenarioResult()
        select mappedResult;

    private Either<EgError, ImmutableList<TopicEntity>> GetTopicEntities(string namePredicate, ExercisesContext ctx)
    {
        if (string.IsNullOrEmpty(namePredicate))
        {
            return repository.FindAllWithBooksIncluded(ctx);
        }

        return repository.FindAllWithBooksByNamePredicate(namePredicate, ctx);
    }
}

public static class FindAllTopicsByNamePredicateScenarioExtensions
{
    public static Either<EgError, ImmutableList<FindAllTopicsByNamePredicateScenarioResult>>
        ToFindAllTopicsByNamePredicateScenarioResult(
            this ImmutableList<TopicEntity> topicEntities
        ) =>
        toSeq(topicEntities)
            .Traverse(item => item.ToFindAllTopicsByNamePredicateScenarioResult())
            .Map(items => items.ToImmutableList())
            .As();

    public static Either<EgError, FindAllTopicsByNamePredicateScenarioResult>
        ToFindAllTopicsByNamePredicateScenarioResult(
            this TopicEntity topicEntity
        ) => new FindAllTopicsByNamePredicateScenarioResult(
        topicEntity.Id,
        topicEntity.Name,
        topicEntity.Reference,
        topicEntity.Books
    );
}

public record FindAllTopicsByNamePredicateScenarioResult(
    long Id,
    string Name,
    string Reference,
    List<BookEntity> Books
);

public record FindAllTopicsByNamePredicateScenarioInput(
    string NamePredicate);