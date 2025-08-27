namespace EncyclopediaGalactica.Scenarios.Exercises.Section.Add;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Section;
using static Prelude;

public class AddNewSectionScenario(
    AddNewSectionScenarioInputValidation validator,
    SectionRepository sectionRepository
)
{
    public Either<EgError, SectionEntity> Execute(
        Section parsedSection,
        long chapterId,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInputToEntity(parsedSection, chapterId)
        from validatedInput in ValidateInput(mappedInput)
        from newEntity in SaveEntity(validatedInput, ctx)
        select newEntity;

    private Either<EgError, SectionEntity> SaveEntity(
        SectionEntity entity,
        ExercisesContext ctx
    ) =>
        sectionRepository.Add(entity, ctx);

    private Either<EgError, SectionEntity> ValidateInput(
        SectionEntity mappedInput
    ) =>
        validator.IsValid(mappedInput);

    private Either<EgError, SectionEntity> MapInputToEntity(
        Section parsedSection,
        long chapterId
    ) =>
        parsedSection.ToEntity().Match(
            Right: result =>
            {
                result.ChapterId = chapterId;
                return Right<EgError, SectionEntity>(result);
            },
            Left: ex => Left(
                new EgError($"Error happened while mapping {nameof(ChapterEntity)}. Error: {ex.Message}")
            )
        );
}