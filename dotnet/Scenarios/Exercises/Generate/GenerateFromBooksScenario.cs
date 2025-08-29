namespace EncyclopediaGalactica.Scenarios.Exercises.Generate;

using System.Collections.Immutable;
using Common;
using Logic.CatalogParser;
using Logic.Generators.LaTeX;
using Logic.Repository.Exercise;
using Logic.Repository.Models;
using Logic.Sync;

public class GenerateFromBooksScenario(
    SyncFsWithDb syncFsWithDb,
    ExerciseRepository exerciseRepository,
    LaTeXGenerator latexGenerator
)
{
    private readonly Random _random = new();

    public Either<EgError, int> Execute(GenerateFromBooksScenarioInput parameters)
    {
        Either<EgError, Unit> scenarioResult =
            from exerciseRecord in CatalogParser.Parse(parameters.BookCatalogPath!)
            from syncResult in syncFsWithDb.Execute(exerciseRecord)
            from selectedExercises in SelectExercises(parameters)
            from enrichedExercises in EnrichSelectedExercises(selectedExercises)
                .Do((res) =>
                    {
                        res.ForEach(item =>
                            {
                                Console.WriteLine(
                                    $"id: {item.Id} " +
                                    $"topic id: {item.TopicId} " +
                                    $"topic name: {item.Topic.Name} "
                                );
                            }
                        );
                    }
                )
            from createdFiles in CreateLatexFile(enrichedExercises)
            select Unit.Default;

        return scenarioResult.Match(
            Right: yolo =>
            {
                return 1;
            },
            Left: nopes =>
            {
                Console.WriteLine($"Errors: {nopes.Message}");
                return 0;
            }
        );
    }

    private Either<EgError, Unit> CreateLatexFile(ImmutableList<ExerciseEntity> exercises) =>
        latexGenerator.Execute(exercises);

    private Either<EgError, ImmutableList<ExerciseEntity>> EnrichSelectedExercises(
        ImmutableList<ExerciseEntity> selectedExercises
    )
    {
        ImmutableList<long> exerciseIds = selectedExercises.Select(item => item.Id)
            .ToImmutableList();
        return from enrichedList in exerciseRepository.EnrichExercises(exerciseIds)
               select enrichedList;
    }

    private Either<EgError, ImmutableList<ExerciseEntity>> SelectExercises(
        GenerateFromBooksScenarioInput parameters
    )
    {
        Console.WriteLine($"input: {parameters.BookCatalogPath!}");
        Console.WriteLine($"input: {parameters.ApplicationQuestionVolume}");
        Console.WriteLine($"input: {parameters.Books}");
        return from booksListInParam in ExtractBooksFromParamForQuery(parameters.Books)
               from exercisesAcrossBooks in GetExercisesAcrossBooks(booksListInParam)
               from selectedApplicationExercises in SelectApplicationExercises(exercisesAcrossBooks, parameters)
               from skillExercisesAddedList in SelectAndAppendSkillExercises(
                   exercisesAcrossBooks,
                   selectedApplicationExercises,
                   parameters
               )
               from selectedConceptsAdded in SelectAndAppendConceptExercises(
                   exercisesAcrossBooks,
                   skillExercisesAddedList,
                   parameters
               )
               from selectedDiscussionsAdded in SelectAndAppendDiscussionExercises(
                   exercisesAcrossBooks,
                   selectedConceptsAdded,
                   parameters
               )
               select selectedDiscussionsAdded;
    }

    private Either<EgError, ImmutableList<ExerciseEntity>> SelectAndAppendDiscussionExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        ImmutableList<ExerciseEntity> selectedConceptsAdded,
        GenerateFromBooksScenarioInput parameters
    )
    {
        List<ExerciseEntity> discussions = exercisesAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Discussion)
            .OrderBy(_ => _random.Next())
            .Take(parameters.DiscussionQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> result = selectedConceptsAdded.AddRange(discussions);
        return Right(result);
    }

    private Either<EgError, ImmutableList<ExerciseEntity>> SelectAndAppendConceptExercises(
        List<ExerciseEntity> exerciseAcrossBooks,
        ImmutableList<ExerciseEntity> selectedApplicationExercises,
        GenerateFromBooksScenarioInput parameters
    )
    {
        List<ExerciseEntity> conceptExercises = exerciseAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Concept)
            .OrderBy(_ => _random.Next())
            .Take(parameters.ConceptQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> result = selectedApplicationExercises.AddRange(conceptExercises);
        return Right(result);
    }

    private Either<EgError, ImmutableList<ExerciseEntity>> SelectAndAppendSkillExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        ImmutableList<ExerciseEntity> toAppendTo,
        GenerateFromBooksScenarioInput parameters
    )
    {
        List<ExerciseEntity> skillExercises = exercisesAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Skill)
            .OrderBy(_ => _random.Next())
            .Take(parameters.SkillQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> appended = toAppendTo.AddRange(skillExercises);
        return Right(appended);
    }

    private Either<EgError, ImmutableList<ExerciseEntity>> SelectApplicationExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        GenerateFromBooksScenarioInput parameters
    )
    {
        ImmutableList<ExerciseEntity> result = exercisesAcrossBooks
            .Where(w => w.ExerciseType == ExerciseType.Application)
            .OrderBy(_ => _random.Next())
            .Take(parameters.ApplicationQuestionVolume)
            .ToImmutableList();
        return Right(result);
    }

    private Either<EgError, List<ExerciseEntity>> GetExercisesAcrossBooks(string[] booksListInParam)
    {
        Console.WriteLine($"parameter: {booksListInParam[0]}");
        return from exercises in exerciseRepository.FindByBookReferences(booksListInParam)
               select exercises;
    }

    private static Either<EgError, string[]> ExtractBooksFromParamForQuery(
        string booksParameter
    )
    {
        try
        {
            string[] books = booksParameter.Split(",");
            return Right(books);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"{nameof(GetExercisesAcrossBooks)}: {e.Message}",
                    e.StackTrace
                )
            );
        }
    }
}

public record GenerateFromBooksScenarioInput
{
    public GenerateFromBooksScenarioInput(
        int skillQuestionVolume,
        int applicationQuestionVolume,
        int conceptQuestionVolume,
        int discussionQuestionVolume,
        string books
    )
    {
        SkillQuestionVolume = skillQuestionVolume;
        ApplicationQuestionVolume = applicationQuestionVolume;
        ConceptQuestionVolume = conceptQuestionVolume;
        DiscussionQuestionVolume = discussionQuestionVolume;
        Books = books;
    }

    public int SkillQuestionVolume { get; set; }
    public int ApplicationQuestionVolume { get; set; }
    public int ConceptQuestionVolume { get; set; }
    public int DiscussionQuestionVolume { get; set; }
    public string Books { get; set; }
    public string? BookCatalogPath { get; set; }
}