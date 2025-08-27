namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Models;

public class SectionEntity
{
    public long Id { get; set; }
    public string? Title { get; set; }

    public double SectionNumber { get; set; }

    public int PageStart { get; set; }

    public int PageExercisesStart { get; set; }

    public int ConceptQuestionsIntervalStart { get; set; }

    public int ConceptQuestionsIntervalEnd { get; set; }

    public int SkillQuestionsIntervalStart { get; set; }

    public int SkillQuestionsIntervalEnd { get; set; }

    public int ApplicationQuestionsIntervalStart { get; set; }

    public int ApplicationQuestionsIntervalEnd { get; set; }

    public int DiscussionQuestionsIntervalStart { get; set; }

    public int DiscussionQuestionsIntervalEnd { get; set; }

    public int PageEnd { get; set; }
    public long ChapterId { get; set; }
    public ChapterEntity Chapter { get; set; }

    public List<ExerciseEntity> Exercises { get; set; } = [];
}