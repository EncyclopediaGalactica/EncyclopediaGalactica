namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Exercise;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class ExerciseRepository
{
    public Either<EgError, List<ExerciseEntity>> FindByBookReferences(string[] bookReferences)
    {
        using ExercisesContext ctx = new(dbContextOptions);
        try
        {
            List<ExerciseEntity> exercises = ctx.Exercises.Include(i => i.Book)
                .Where(w => bookReferences.Contains(w.Book.Reference))
                .ToList();
            Console.WriteLine($"size: {exercises.Count}");
            return Right(exercises);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}