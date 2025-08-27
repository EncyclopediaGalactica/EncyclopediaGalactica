namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Update;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Chapter;
using Logic.Repository.Models;
using static Prelude;

public class UpdateChapterScenario(
    UpdateChapterScenarioInputValidator validator,
    ChapterRepository chapterRepository
)
{
    public Either<EgError, ChapterEntity> Execute(
        Chapter parsedChapter,
        long bookId,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInputToEntity(parsedChapter, bookId)
        from validatedInput in ValidateInput(mappedInput)
        from updatedEntity in UpdateEntity(validatedInput, ctx)
        select updatedEntity;

    private Either<EgError, ChapterEntity> UpdateEntity(
        ChapterEntity entity,
        ExercisesContext ctx
    ) =>
        chapterRepository.Update(entity, ctx);

    private Either<EgError, ChapterEntity> ValidateInput(ChapterEntity input) =>
        validator.IsValid(input);

    private Either<EgError, ChapterEntity> MapInputToEntity(
        Chapter parsedChapter,
        long bookId
    ) => parsedChapter.ToChapterEntity().Match(
        Right: res =>
        {
            res.BookId = bookId;
            return Right<EgError, ChapterEntity>(res);
        },
        Left:
        ex => Left(new EgError($"Chapter update error: {ex.Message}"))
    );
}