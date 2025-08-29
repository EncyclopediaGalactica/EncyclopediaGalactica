namespace EncyclopediaGalactica.CommandLineInterface.Cli.Exercises.Generate.Book;

using System.ComponentModel;
using Common;
using Infrastructure;
using Scenarios.Exercises.Generate;
using Scenarios.Exercises.Logic.CatalogParser;
using Scenarios.Exercises.Logic.Sync;
using Spectre.Console.Cli;

[Description("Generate test sheets from the catalogised text books.")]
public sealed class BooksCommand(
    GenerateFromBooksScenario generateFromBooksScenario,
    SyncFsWithDb syncFsWithDb,
    ExercisesSettings appSettings
) : Command<BooksCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        string bookCatalogPath = appSettings.Exercises!.TextBookCatalogPath!;
        Either<EgError, int> executionResult =
            from parsed in CatalogParser.Parse(bookCatalogPath)
            from syncResult in syncFsWithDb.Execute(parsed)
            from mappedInput in settings.ToGenerateFromBooksScenarioInput()
            let catalogPathAdded = mappedInput with { BookCatalogPath = bookCatalogPath, }
            from scenarioResult in generateFromBooksScenario.Execute(catalogPathAdded)
            select scenarioResult;
        return executionResult.Match(
            Right: _ =>
            {
                Console.WriteLine("executed");
                return 0;
            },
            Left: nopes =>
            {
                Console.WriteLine($"nopes: {nopes.Message}, {nopes.Trace}");
                return 1;
            }
        );
    }

    public sealed class Settings : GenerateSettings
    {
        [CommandOption("-b|--books", true)]
        [Description("Command separated list of book references where the generated exercises coming from.")]
        public string Books { get; set; } = string.Empty;

        [CommandOption("-s|--skill")]
        [Description("How many skill questions will be included in the result test.")]
        public int SkillQuestionVolume { get; set; }


        [CommandOption("-a|--app")]
        [Description("How many application questions will be included in the result test.")]
        public int ApplicationQuestionVolume { get; set; }


        [CommandOption("-c|--concept")]
        [Description("How many concept questions will be included in the result test.")]
        public int ConceptQuestionVolume { get; set; }


        [CommandOption("-d|--discussion")]
        [Description("How many discussion questions will be included in the result test.")]
        public int DiscussionQuestionVolume { get; set; }
    }
}

public static class BooksCommandSettingsExtensions
{
    public static Either<EgError, GenerateFromBooksScenarioInput> ToGenerateFromBooksScenarioInput(
        this BooksCommand.Settings settings
    )
    {
        try
        {
            return Right(
                new GenerateFromBooksScenarioInput(
                    settings.SkillQuestionVolume,
                    settings.ApplicationQuestionVolume,
                    settings.ConceptQuestionVolume,
                    settings.DiscussionQuestionVolume,
                    settings.Books
                )
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}