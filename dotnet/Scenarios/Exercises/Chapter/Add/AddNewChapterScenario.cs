namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Add;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Chapter;
using Logic.Repository.Models;
using static Prelude;

public class AddNewChapterScenario(
    AddNewChapterScenarioInputValidator validator,
    ChapterRepository chapterRepository
)
{
    public Either<EgError, ChapterEntity> Execute(
        Chapter parsedChapter,
        long bookId,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInput(parsedChapter, bookId)
        from validatedInput in ValidateInput(mappedInput)
        from newChapterEntity in Save(validatedInput, ctx)
        select newChapterEntity;

    private Either<EgError, ChapterEntity> Save(
        ChapterEntity input,
        ExercisesContext ctx
    ) =>
        chapterRepository.Add(input, ctx);

    private Either<EgError, ChapterEntity> ValidateInput(ChapterEntity parsedChapter) =>
        validator.IsValid(parsedChapter);

    private Either<EgError, ChapterEntity> MapInput(
        Chapter parsedChapter,
        long bookId
    ) => parsedChapter.ToChapterEntity().Match(
        Right: result =>
        {
            result.BookId = bookId;
            return Right<EgError, ChapterEntity>(result);
        },
        Left: error => error
    );
}