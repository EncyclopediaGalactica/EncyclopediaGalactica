namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Section;

using Common;
using Models;
using static Prelude;

public partial class SectionRepository
{
    public Either<EgError, Option<SectionEntity>> FindSectionByChapterIdAndSectionNumber(
        long chapterId,
        double sectionNumber,
        ExercisesContext ctx
    )
    {
        try
        {
            SectionEntity? target = ctx.Sections
                .FirstOrDefault(w => w.ChapterId == chapterId && w.SectionNumber == sectionNumber);
            return target == null ? None : Some(target);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"Error happened while looking " +
                    $"for {nameof(SectionEntity)} by " +
                    $"{nameof(ChapterEntity)}.{nameof(ChapterEntity.Id)} with value: {chapterId} and " +
                    $"{nameof(SectionEntity.SectionNumber)} with value: {sectionNumber}. " +
                    $"Error: {e.Message}"
                )
            );
        }
    }
}