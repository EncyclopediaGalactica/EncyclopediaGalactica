namespace EncyclopediaGalactica.Scenarios.Exercises.Section.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Section;

public class FindSectionByChapterIdAndSectionNumberScenario(
    SectionRepository sectionRepository
)
{
    public Either<EgError, Option<SectionEntity>> Execute(
        long chapterId,
        double sectionNumber,
        ExercisesContext ctx
    ) =>
        sectionRepository.FindSectionByChapterIdAndSectionNumber(chapterId, sectionNumber, ctx);
}