namespace EncyclopediaGalactica.Scenarios.Exercises.Exercise;

using Common;
using Logic.Repository;
using Logic.Repository.Exercise;
using Logic.Repository.Models;

public class ListExercisesScenario(
    ExerciseRepository exerciseRepository,
    ExercisesContext ctx
)
{
    public Either<EgError, List<ListExercisesScenarioResult>> Execute(
        ListExercisesScenarioInput input
    ) =>
        from mappedInput in input.ToFindByBookTitleOrChapterTitleInput()
        from exerciseList in exerciseRepository.FindByBookTitleOrChapterTitle(mappedInput, ctx)
        from mappedResultList in exerciseList.ToListExercisesScenarioResultList()
        select mappedResultList;
}

public static class ListExercisesScenarioExtensions
{
    public static Either<EgError, List<ListExercisesScenarioResult>> ToListExercisesScenarioResultList(
        this List<ExerciseEntity> input
    ) =>
        toSeq(input)
            .Traverse(item => item.ToListExercisesScenarioResult())
            .Map(list => list.ToList())
            .As();

    public static Either<EgError, ListExercisesScenarioResult> ToListExercisesScenarioResult(this ExerciseEntity entity)
    {
        try
        {
            return Right(
                new ListExercisesScenarioResult(
                    entity.Id,
                    entity.IdInTheBook,
                    entity.BookId,
                    entity.Book.Title,
                    entity.ChapterId,
                    entity.Chapter.Title
                )
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, FindByBookTitleOrChapterTitleInput> ToFindByBookTitleOrChapterTitleInput(
        this ListExercisesScenarioInput input
    )
    {
        try
        {
            return Right(
                new FindByBookTitleOrChapterTitleInput(
                    input.BookTitleFilter,
                    input.ChapterTitleFilter
                )
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}

public record ListExercisesScenarioResult(
    long Id,
    long IdInTheBook,
    long BookId,
    string BookTitle,
    long ChapterId,
    string ChapterTitle
);

public record ListExercisesScenarioInput(
    string BookTitleFilter,
    string ChapterTitleFilter);