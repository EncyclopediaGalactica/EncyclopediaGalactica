namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Exercise;

using Common;
using Models;

public partial class ExerciseRepository
{
    public Either<EgError, ExerciseEntity> Add(
        ExerciseEntity mappedInput,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Exercises.Add(mappedInput);
            ctx.SaveChanges();
            return Right(mappedInput);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}