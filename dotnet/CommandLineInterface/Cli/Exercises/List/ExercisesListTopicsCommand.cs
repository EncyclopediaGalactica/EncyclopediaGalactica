namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.List;

using System.Collections.Immutable;
using System.ComponentModel;
using Common;
using Scenarios.Exercises.Logic.Repository;
using Scenarios.Exercises.Topic.Find;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Lists the topics in the Exercises domain")]
public sealed class ExercisesListTopicsCommand(
    FindAllTopicsByNamePredicateScenario findAllTopicsByNamePredicateScenario,
    ExercisesContext ctx
) : Command<ExercisesListTopicsCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, ImmutableList<FindAllTopicsByNamePredicateScenarioResult>> result =
            from mappedInput in settings.ToFindAllTopicsByNamePredicateScenarioInput()
            from scenarioResult in findAllTopicsByNamePredicateScenario.Execute(mappedInput, ctx)
            select scenarioResult;
        return result.Match(
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

    private Either<EgError, Unit> RenderResult(ImmutableList<FindAllTopicsByNamePredicateScenarioResult> result)
    {
        try
        {
            Table table = new();
            table
                .AddColumn("Id")
                .AddColumn("Name")
                .AddColumn("Reference")
                .AddColumn("Books");
            result.ForEach(item =>
                {
                    string books = "N/A";
                    if (item.Books.Count > 0)
                    {
                        books = string.Join(", ", item.Books.Select(b => b.Title));
                    }

                    table.AddRow(item.Id.ToString(), item.Name, item.Reference, books);
                }
            );
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }

    public sealed class Settings : ListSettings
    {
        [CommandOption("-f|--filter", false)]
        [Description("Lists only the topics match for the predicate. Topic matchings is LIKE %{topic}%")]
        public string TopicName { get; set; } = string.Empty;
    }
}

public static class ExercisesListTopicsCommandExtensions
{
    public static Either<EgError, FindAllTopicsByNamePredicateScenarioInput>
        ToFindAllTopicsByNamePredicateScenarioInput(
            this ExercisesListTopicsCommand.Settings settings
        )
    {
        try
        {
            return Right(new FindAllTopicsByNamePredicateScenarioInput(settings.TopicName));
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}