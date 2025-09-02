namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.ComponentModel;
using Common;
using Scenarios.Exercises.Exercise;
using Spectre.Console;
using Spectre.Console.Cli;

public sealed class ExercisesListExercisesCommand(
    ListExercisesScenario listExercisesScenario
) : Command<ExercisesListExercisesCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, List<ListExercisesScenarioResult>> executionResult =
            from mappedInput in settings.ToListExercisesScenarioInput()
            from result in listExercisesScenario.Execute(mappedInput)
            select result;
        return executionResult.Match(
            Right: yolo =>
            {
                return RenderResult(yolo)
                    .Match(
                        Right: _ => 0,
                        Left: nopesNopes =>
                        {
                            EgCli.RenderError(nopesNopes);
                            return 1;
                        }
                    );
            },
            Left: nopes =>
            {
                EgCli.RenderError(nopes);
                return 1;
            }
        );
    }

    private Either<EgError, Unit> RenderResult(List<ListExercisesScenarioResult> result)
    {
        try
        {
            Table table = new();
            table
                .AddColumn("Id")
                .AddColumn("Id in the book")
                .AddColumn("Book title")
                .AddColumn("Chapter title");
            result.ForEach(item =>
                {
                    table.AddRow(
                        item.Id.ToString(),
                        item.IdInTheBook.ToString(),
                        item.BookTitle.ToString(),
                        item.ChapterTitle.ToString()
                    );
                }
            );
            AnsiConsole.Write(table);
            return Unit.Default;
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
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