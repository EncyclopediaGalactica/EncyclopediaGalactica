namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.ComponentModel;
using EncyclopediaGalactica.Common;
using EncyclopediaGalactica.Scenarios.Exercises.Exercise;
using Spectre.Console.Cli;

public sealed class ExercisesListExercisesCommand(
        ListExercisesScenario listExercisesScenario
        ) : Command<ExercisesListExercisesCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, List<ListExercisesScenarioResult>> result = listExercisesScenario.Execute();
    }

    public sealed class Settings : ListSettings
    {
        [CommandOption("--chapter-title-filter")]
        [Description("Filter exercises by chapter title")]
        public string? ChapterTitleFilter { get; set; }

        [CommandOption("--book-title-filter")]
        [Description("Filter exercises by book")]
        public string? BookTitleFilter { get; set; }
    }

}

public static class ExercisesListExercisesCommandExtensions
{
    public static Either<EgError, ListExercisesScenarioInput> ToListExercisesScenarioInput(
        this ExercisesListExercisesCommand.Settings settings
    )
    {
        try
        {
            return Right(
                new ListExercisesScenarioInput(
                    string.IsNullOrEmpty(settings.BookTitleFilter) ? string.Empty : settings.BookTitleFilter,
                    string.IsNullOrEmpty(settings.ChapterTitleFilter) ? string.Empty : settings.ChapterTitleFilter
                )
            );
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }
}