namespace EncyclopediaGalactica.Scenarios.Exercises.Chapter.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Chapter;
using Logic.Repository.Models;

public class FindChapterByReferenceScenario(
    ChapterRepository chapterRepository
)
{
    public Either<EgError, Option<ChapterEntity>> Execute(
        string reference,
        ExercisesContext ctx
    ) =>
        from countOfChaptersByReference in chapterRepository.CountByReference(reference, ctx)
        let isOne = match(
            countOfChaptersByReference == 1
                ? Either<EgError, int>.Right(1)
                : Either<EgError, int>.Left(
                    new EgError(
                        $"Requesting chapter by reference: {reference} resulted in {countOfChaptersByReference}. " +
                        $"This should be 1."
                    )
                ),
            nopes => Left(nopes),
            yolo => Right(yolo)
        )
        from chapterEntity in chapterRepository.FindByReference(reference, ctx)
        select chapterEntity;
}