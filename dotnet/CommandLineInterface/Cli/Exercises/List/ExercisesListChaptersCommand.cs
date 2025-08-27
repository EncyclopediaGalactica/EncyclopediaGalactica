namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.Collections.Immutable;
using System.ComponentModel;
using Common;
using Scenarios.Exercises.Chapter.Find;
using Spectre.Console;
using Spectre.Console.Cli;

public sealed class ExercisesListChaptersCommand(
    FindChaptersByTitlePredicateAndBookAndTopicScenario findChaptersByTitlePredicateAndBookAndTopicScenario)
    : Command<ExercisesListChaptersCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, ImmutableList<FindChapterByTitlePredicateAndBookAndTopicResult>> dataResult =
            from mappedSettings in settings.ToFindChapterByTitlePredicateAndBookAndTopicScenarioInput()
            from r in findChaptersByTitlePredicateAndBookAndTopicScenario.Execute(mappedSettings)
            select r;

        return dataResult.Match(
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

    private Either<EgError, Unit> RenderResult(ImmutableList<FindChapterByTitlePredicateAndBookAndTopicResult> result)
    {
        try
        {
            Table table = new();
            table
                .AddColumn("Id")
                .AddColumn("Title")
                .AddColumn("Reference")
                .AddColumn("Book Title")
                .AddColumn("Topic");
            result.ForEach(item =>
                {
                    table.AddRow(item.Id.ToString(), item.Title, item.Reference, item.BookTitle, item.TopicName);
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
        [Description("Lists only the chapters match for the predicate. Topic matchings is LIKE %{chapter}%")]
        public string TopicName { get; set; } = string.Empty;
    }
}

public static class ExercisesListChaptersCommandExtensions
{
    public static Either<EgError, FindChapterByTitlePredicateAndBookAndTopicScenarioInput>
        ToFindChapterByTitlePredicateAndBookAndTopicScenarioInput(this ExercisesListChaptersCommand.Settings settings)
        => new FindChapterByTitlePredicateAndBookAndTopicScenarioInput(settings.TopicName);
}