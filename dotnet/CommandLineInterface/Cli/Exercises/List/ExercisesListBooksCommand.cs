namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.ComponentModel;
using Common;
using Microsoft.Extensions.Logging;
using Scenarios.Exercises.Book.List;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Lists catalogised books.")]
public sealed class ExercisesListBooksCommand(
    ListBooksByTopicScenario listBooksByTopicScenario,
    ILogger<ExercisesListBooksCommand> logger
) : Command<ExercisesListBooksCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, List<ListBooksByTopicScenarioResult>> result =
            from mappedInput in settings.ToListBooksByTopicScenarioInput()
            from list in listBooksByTopicScenario.Execute(mappedInput)
            select list;
        return result.Match(
            Right: yolo =>
            {
                logger.LogDebug("Result count: {YoloCount}", yolo.Count);
                return RenderResult(yolo).Match(
                    Right: _ =>
                    {
                        logger.LogDebug("Result rendered:");
                        return 0;
                    },
                    Left: nopesNopes =>
                    {
                        logger.LogDebug("Render error: {error} and {trace}", nopesNopes.Message, nopesNopes.Trace);
                        return 1;
                    }
                );
            },
            Left: nopes =>
            {
                logger.LogDebug("Failed with error: {error} and {trace}", nopes.Message, nopes.Trace);
                EgCli.RenderError(nopes);
                return 1;
            }
        );
    }

    private Either<EgError, Unit> RenderResult(List<ListBooksByTopicScenarioResult> result)
    {
        try
        {
            Table table = new();
            table
                .AddColumn("Id")
                .AddColumn("Title")
                .AddColumn("Author")
                .AddColumn("Reference")
                .AddColumn("Topic");
            result.ForEach(item =>
                {
                    string topicName = string.IsNullOrEmpty(item.TopicName) ? "not defined" : item.TopicName;
                    table.AddRow(
                        item.BookId.ToString(),
                        item.BookTitle,
                        item.Author,
                        item.BookReference,
                        topicName
                    );
                }
            );
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"{nameof(ExercisesListBooksCommand)}.{nameof(RenderResult)}",
                    e.StackTrace
                )
            );
        }
    }

    public sealed class Settings : ListSettings
    {
        [CommandOption("-t|--topic", false)]
        [Description("List only the books in the topic. Topic matchins is LIKE %{topic}%")]
        public string TopicName { get; set; } = string.Empty;
    }
}

public static class ListBookCommandExtensions
{
    public static Either<EgError, ListBooksByTopicScenarioInput> ToListBooksByTopicScenarioInput(
        this ExercisesListBooksCommand.Settings settings
    )
    {
        try
        {
            return Right(new ListBooksByTopicScenarioInput() { TopicName = settings.TopicName, });
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"{nameof(ListBookCommandExtensions)}.{nameof(ToListBooksByTopicScenarioInput)}: error: {e.Message}",
                    e.StackTrace
                )
            );
        }
    }
}