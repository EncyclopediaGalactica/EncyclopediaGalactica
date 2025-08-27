namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Topic;
using static Prelude;

public class FindTopicByNameAndReferenceScenario(
    TopicRepository topicRepository)
{
    public Either<EgError, Option<TopicEntity>> Execute(
        string name,
        string reference,
        ExercisesContext dbContext
    )
    {
        if (string.IsNullOrWhiteSpace(name)
            || string.IsNullOrEmpty(name)
            || string.IsNullOrWhiteSpace(reference)
            || string.IsNullOrEmpty(reference))
        {
            return Left(new EgError($"Either name or reference wasn't provided."));
        }

        return from r in topicRepository.FindByNameAndReference(name, reference, dbContext)
            select r;
    }
}