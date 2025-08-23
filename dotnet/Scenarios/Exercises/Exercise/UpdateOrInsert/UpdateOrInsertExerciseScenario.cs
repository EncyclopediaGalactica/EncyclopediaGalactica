namespace EncyclopediaGalactica.Scenarios.Exercises.Exercise.UpdateOrInsert;

using Common;
using Logic.Repository;
using Logic.Repository.Exercise;
using Logic.Repository.Models;

public class UpdateOrInsertExerciseScenario(
    UpdateExerciseScenarioInputValidator updateExerciseScenarioInputValidator,
    AddExerciseScenarioInputValidator addExerciseScenarioInputValidator,
    ExerciseRepository exerciseRepository
)
{
    public Either<EgError, ExerciseEntity> Execute(
        long topicId,
        long bookId,
        long chapterId,
        long chapterIdInTheBook,
        long sectionId,
        double sectionIdInTheBook,
        long idInTheBook,
        ExerciseType exerciseType,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInputToEntity(
            topicId,
            bookId,
            chapterId,
            chapterIdInTheBook,
            sectionId,
            sectionIdInTheBook,
            idInTheBook,
            exerciseType
        )
        from findResult in FindEntity(mappedInput, ctx).Match(
            Right: update =>
            {
                Either<EgError, ExerciseEntity> result = exerciseRepository.Update(update.Id, mappedInput, ctx);
                return result;
            },
            Left: _ =>
            {
                Either<EgError, ExerciseEntity> result = exerciseRepository.Add(mappedInput, ctx);
                return result;
            }
        )
        select findResult;


    private Either<EgError, ExerciseEntity> UpdateEntity(
        ExerciseEntity entity,
        ExercisesContext ctx
    ) =>
        exerciseRepository.Update(entity.Id, entity, ctx);

    private Either<EgError, ExerciseEntity> FindEntity(
        ExerciseEntity mappedInput,
        ExercisesContext ctx
    ) =>
        exerciseRepository.Find(mappedInput, ctx)
            .Match(
                Right: result => Right<EgError, ExerciseEntity>(result),
                Left: _ => Left<EgError, ExerciseEntity>(new EgError($""))
            );

    private Either<EgError, ExerciseEntity> MapInputToEntity(
        long topicId,
        long bookId,
        long chapterId,
        long chapterIdInTheBook,
        long sectionId,
        double sectionIdInTheBook,
        long idInTheBook,
        ExerciseType exerciseType
    ) =>
        new ExerciseEntity()
        {
            Id = 0,
            IdInTheBook = idInTheBook,
            SectionId = sectionId,
            SectionIdInThebook = sectionIdInTheBook,
            ChapterId = chapterId,
            ChapterIdInTheBook = chapterIdInTheBook,
            BookId = bookId,
            TopicId = topicId,
            ExerciseType = exerciseType,
        };
}