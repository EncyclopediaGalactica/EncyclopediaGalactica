namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.CatalogParser.Model;

using Common;
using Repository.Models;
using YamlDotNet.Serialization;
using static Prelude;

public class Section
{
    public string? Title { get; set; }

    [YamlMember(Alias = "section_number", ApplyNamingConventions = false)]
    public double SectionNumber { get; set; }

    [YamlMember(Alias = "page_start", ApplyNamingConventions = false)]
    public int PageStart { get; set; }

    [YamlMember(Alias = "page_exercises_start", ApplyNamingConventions = false)]
    public string PageExercisesStart { get; set; } = string.Empty;

    [YamlMember(Alias = "concepts_questions_interval_start", ApplyNamingConventions = false)]
    public string ConceptQuestionsIntervalStart { get; set; } = string.Empty;

    [YamlMember(Alias = "concepts_questions_interval_end", ApplyNamingConventions = false)]
    public string ConceptQuestionsIntervalEnd { get; set; } = string.Empty;

    [YamlMember(Alias = "skills_questions_interval_start", ApplyNamingConventions = false)]
    public string SkillQuestionsIntervalStart { get; set; } = string.Empty;

    [YamlMember(Alias = "skills_questions_interval_end", ApplyNamingConventions = false)]
    public string SkillQuestionsIntervalEnd { get; set; } = string.Empty;

    [YamlMember(Alias = "applications_questions_interval_start", ApplyNamingConventions = false)]
    public string ApplicationQuestionsIntervalStart { get; set; } = string.Empty;

    [YamlMember(Alias = "applications_questions_interval_end", ApplyNamingConventions = false)]
    public string ApplicationQuestionsIntervalEnd { get; set; } = string.Empty;

    [YamlMember(Alias = "discussion_questions_interval_start", ApplyNamingConventions = false)]
    public string DiscussionQuestionsIntervalStart { get; set; } = string.Empty;

    [YamlMember(Alias = "discussion_questions_interval_end", ApplyNamingConventions = false)]
    public string DiscussionQuestionsIntervalEnd { get; set; } = string.Empty;

    [YamlMember(Alias = "page_end", ApplyNamingConventions = false)]
    public int PageEnd { get; set; }

    [YamlMember(Alias = "chapter_reference", ApplyNamingConventions = false)]
    public string ChapterReference { get; set; } = string.Empty;
}

public static class SectionExtensions
{
    public static Either<EgError, SectionEntity> ToEntity(
        this Section parsedSection
    )
    {
        try
        {
            SectionEntity result = new()
            {
                Title = parsedSection.Title,
                SectionNumber = parsedSection.SectionNumber,
                PageStart = parsedSection.PageStart,
                PageExercisesStart = parsedSection.PageExercisesStart == "NA"
                    ? 0
                    : int.Parse(parsedSection.PageExercisesStart),
                ConceptQuestionsIntervalStart = parsedSection.ConceptQuestionsIntervalStart == "NA"
                    ? 0
                    : int.Parse(parsedSection.ConceptQuestionsIntervalStart),
                ConceptQuestionsIntervalEnd = parsedSection.ConceptQuestionsIntervalEnd == "NA"
                    ? 0
                    : int.Parse(parsedSection.ConceptQuestionsIntervalEnd),
                SkillQuestionsIntervalStart = parsedSection.SkillQuestionsIntervalStart == "NA"
                    ? 0
                    : int.Parse(parsedSection.SkillQuestionsIntervalStart),
                SkillQuestionsIntervalEnd = parsedSection.SkillQuestionsIntervalEnd == "NA"
                    ? 0
                    : int.Parse(parsedSection.SkillQuestionsIntervalEnd),
                ApplicationQuestionsIntervalStart = parsedSection.ApplicationQuestionsIntervalStart == "NA"
                    ? 0
                    : int.Parse(parsedSection.ApplicationQuestionsIntervalStart),
                ApplicationQuestionsIntervalEnd = parsedSection.ApplicationQuestionsIntervalEnd == "NA"
                    ? 0
                    : int.Parse(parsedSection.ApplicationQuestionsIntervalEnd),
                DiscussionQuestionsIntervalStart = parsedSection.DiscussionQuestionsIntervalStart == "NA"
                    ? 0
                    : int.Parse(parsedSection.DiscussionQuestionsIntervalStart),
                DiscussionQuestionsIntervalEnd = parsedSection.DiscussionQuestionsIntervalEnd == "NA"
                    ? 0
                    : int.Parse(parsedSection.DiscussionQuestionsIntervalEnd),
                PageEnd = parsedSection.PageEnd,
            };
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new EgError(
                    $"Error while mapping {nameof(Section)} to {nameof(SectionEntity)}. Error: {e.Message}"
                )
            );
        }
    }
}