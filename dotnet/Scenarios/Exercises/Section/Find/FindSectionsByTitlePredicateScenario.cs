namespace EncyclopediaGalactica.Scenarios.Exercises.Section.Find;

using System.Collections.Immutable;
using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Section;

public class FindSectionsByTitlePredicateScenario(
    SectionRepository sectionRepository,
    ExercisesContext ctx
)
{
    public Either<EgError, ImmutableList<FindSectionsByTitlePredicateScenarioResult>> Execute(
        FindSectionsByTitlePredicateScenarioInput input
    ) =>
        from rawResult in GetChapterEntities(input.TitlePredicate, ctx)
        from mappedResult in rawResult.ToFindSectionsByTitlePredicateScenarioResultList()
        select mappedResult;

    private Either<EgError, ImmutableList<SectionEntity>> GetChapterEntities(
        string titlePredicate,
        ExercisesContext ctx
    )
    {
        if (string.IsNullOrWhiteSpace(titlePredicate))
        {
            return sectionRepository.FindAllAndChapterAndBookAndTopic(ctx);
        }

        return sectionRepository.FindAllByTitlePredicateAndChapterAndBookAndTopic(titlePredicate, ctx);
    }
}

public record FindSectionsByTitlePredicateScenarioResult(
    long Id,
    string Title,
    string ChapterTitle,
    string BookTitle,
    string TopicTitle);

public record FindSectionsByTitlePredicateScenarioInput(
    string TitlePredicate);

public static class FindSectionsByTitlePredicateScenarioExtensions
{
    public static Either<EgError, ImmutableList<FindSectionsByTitlePredicateScenarioResult>>
        ToFindSectionsByTitlePredicateScenarioResultList(this ImmutableList<SectionEntity> input) => toSeq(input)
        .Traverse(i => i.ToFindSectionsByTitlePredicateScenarioResult())
        .Map(ii => ii.ToImmutableList())
        .As();

    public static Either<EgError, FindSectionsByTitlePredicateScenarioResult>
        ToFindSectionsByTitlePredicateScenarioResult(this SectionEntity input)
    {
        try
        {
            return Right(
                new FindSectionsByTitlePredicateScenarioResult(
                    input.Id,
                    input.Title,
                    input.Chapter != null ? input.Chapter.Title : "N/A",
                    input.Chapter.Book != null ? input.Chapter.Book.Title : "N/A",
                    input.Chapter.Book.Topic != null ? input.Chapter.Book.Title : "N/A"
                )
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}