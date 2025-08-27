namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Exercise;

using System.Collections.Immutable;
using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class ExerciseRepository(
    DbContextOptions<ExercisesContext> dbContextOptions)
{
    public Either<EgError, ImmutableList<ExerciseEntity>> EnrichExercises(ImmutableList<long> exerciseIds)
    {
        try
        {
            using ExercisesContext ctx = new(dbContextOptions);
            ImmutableList<ExerciseEntity> exerciseEntities = ctx.Exercises
                .Include(i => i.Book)
                .Include(i => i.Topic)
                .Include(i => i.Chapter)
                .Include(i => i.Section)
                .Where(i => exerciseIds.Contains(i.Id))
                .ToImmutableList();
            return Right(exerciseEntities);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}