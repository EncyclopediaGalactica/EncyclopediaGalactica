namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Chapter;

using Common;
using Models;
using static Prelude;

public partial class ChapterRepository
{
    public Either<EgError, ChapterEntity> Add(
        ChapterEntity chapter,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Chapters.Add(chapter);
            ctx.SaveChanges();
            return Right(chapter);
        }
        catch (Exception e)
        {
            return Left(
                new EgError($"Error happened while recording {nameof(ChapterEntity)}. Error: {e.Message}")
            );
        }
    }
}