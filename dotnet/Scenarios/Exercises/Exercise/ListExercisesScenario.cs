namespace EncyclopediaGalactica.Scenarios.Exercises.Exercise;

using EncyclopediaGalactica.Common;
using EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Exercise;

public class ListExercisesScenario(
        ExerciseRepository exerciseRepository
        )
{
    public Either<EgError, List<ListExerciseScenarioResult>> Execute() { }

}

public record ListExerciseScenarioResult();
public record ListExercisesScenarioInput(
    string  BookTitleFilter,
    string  ChapterTitleFilter);