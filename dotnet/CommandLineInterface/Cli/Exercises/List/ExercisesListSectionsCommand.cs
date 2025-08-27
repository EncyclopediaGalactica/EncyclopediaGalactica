namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.Collections.Immutable;
using System.ComponentModel;
using Common;
using Scenarios.Exercises.Section.Find;
using Spectre.Console;
using Spectre.Console.Cli;

public sealed class ExercisesListSectionsCommand(
    FindSectionsByTitlePredicateScenario scenario
) : Command<ExercisesListSectionsCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, ImmutableList<FindSectionsByTitlePredicateScenarioResult>> result =
            from mappedInput in settings.ToFindSectionsByTitlePredicateScenarioInput()
            from list in scenario.Execute(mappedInput)
            select list;
        return result.Match(
            Right: yolo =>
            {
                return RenderResult(yolo).Match(
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

    private Either<EgError, Unit> RenderResult(ImmutableList<FindSectionsByTitlePredicateScenarioResult> result)
    {
        try
        {
            Table table = new();
            table
                .AddColumn("Id")
                .AddColumn("Title")
                .AddColumn("Chapter title")
                .AddColumn("Book title")
                .AddColumn("Topic title");
            result.ForEach(item =>
                {
                    table.AddRow(
                        item.Id.ToString(),
                        item.Title,
                        item.ChapterTitle,
                        item.BookTitle,
                        item.TopicTitle
                    );
                }
            );
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public sealed class Settings : ListSettings
    {
        [CommandOption("-f|--filter", false)]
        [Description("Lists only the chapters match to the predicate. Topic matchings is LIKE %{section title}%")]
        public string TopicName { get; set; } = string.Empty;
    }
}

public static class ExercisesListSectionsCommandExtensions
{
    public static Either<EgError, FindSectionsByTitlePredicateScenarioInput>
        ToFindSectionsByTitlePredicateScenarioInput(
            this ExercisesListSectionsCommand.Settings settings
        )
    {
        try
        {
            return Right(
                new FindSectionsByTitlePredicateScenarioInput(settings.TopicName)
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}