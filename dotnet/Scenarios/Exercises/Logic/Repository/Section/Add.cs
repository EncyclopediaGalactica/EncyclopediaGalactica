namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Section;

using Common;
using Models;
using static Prelude;

public partial class SectionRepository
{
    public Either<EgError, SectionEntity> Add(
        SectionEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Sections.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            return Left<EgError, SectionEntity>(
                new EgError($"Error happened while recording {nameof(SectionEntity)}. Error: {e.Message}")
            );
        }
    }
}