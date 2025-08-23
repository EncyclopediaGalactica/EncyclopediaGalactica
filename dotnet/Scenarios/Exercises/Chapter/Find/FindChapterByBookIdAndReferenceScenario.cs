namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Chapter;
using Logic.Repository.Models;

public class FindChapterByBookIdAndReferenceScenario(
    ChapterRepository chapterRepository
)
{
    public Either<EgError, Option<ChapterEntity>> Execute(
        long bookId,
        string reference,
        ExercisesContext ctx
    ) =>
        from chapterEntity in chapterRepository.FindByBookIdAndReference(bookId, reference, ctx)
        select chapterEntity;
}