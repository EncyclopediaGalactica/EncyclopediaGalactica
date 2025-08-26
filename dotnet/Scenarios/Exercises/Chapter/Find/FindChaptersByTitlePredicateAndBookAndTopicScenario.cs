namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Find;

using System.Collections.Immutable;
using Common;
using Logic.Repository;
using Logic.Repository.Chapter;
using Logic.Repository.Models;

public class FindChaptersByTitlePredicateAndBookAndTopicScenario(
    ChapterRepository chapterRepository,
    ExercisesContext ctx
)
{
    public Either<EgError, ImmutableList<FindChapterByTitlePredicateAndBookAndTopicResult>> Execute(
        FindChapterByTitlePredicateAndBookAndTopicScenarioInput input
    ) =>
        from hitList in GetResultFromRepository(input.TitlePredicate, ctx)
        from mappedResult in hitList.ToFindChapterByTitlePredicateAndBookAndTopicResultList()
        select mappedResult;

    private Either<EgError, ImmutableList<ChapterEntity>> GetResultFromRepository(
        string titlePredicate,
        ExercisesContext ctx
    )
    {
        if (string.IsNullOrWhiteSpace(titlePredicate))
        {
            return chapterRepository.FindAll(ctx);
        }

        return chapterRepository.FindByTitlePredicateAndBookAndTopic(titlePredicate, ctx);
    }
}

public static class FindChapterByTitlePredicateAndBookAndTopicScenarioExtensions
{
    public static Either<EgError, ImmutableList<FindChapterByTitlePredicateAndBookAndTopicResult>>
        ToFindChapterByTitlePredicateAndBookAndTopicResultList(this ImmutableList<ChapterEntity> chapters) =>
        toSeq(chapters)
            .Traverse(i => i.ToFindChapterByTitlePredicateAndBookAndTopicResult())
            .Map(result => result.ToImmutableList())
            .As();

    private static Either<EgError, FindChapterByTitlePredicateAndBookAndTopicResult>
        ToFindChapterByTitlePredicateAndBookAndTopicResult(
            this ChapterEntity chapter
        )
    {
        try
        {
            return new FindChapterByTitlePredicateAndBookAndTopicResult(
                chapter.Id,
                chapter.Title,
                chapter.Reference,
                chapter.Book == null ? "N/A" : chapter.Book.Title,
                chapter.Book?.Topic == null ? "N/A" : chapter.Book.Topic.Name
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}

public record FindChapterByTitlePredicateAndBookAndTopicResult(
    long Id,
    string Title,
    string Reference,
    string BookTitle,
    string TopicName
);

public record FindChapterByTitlePredicateAndBookAndTopicScenarioInput(
    string TitlePredicate);