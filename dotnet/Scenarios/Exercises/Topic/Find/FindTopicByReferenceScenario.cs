namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Topic;
using static Prelude;

public class FindTopicByReferenceScenario(
    TopicRepository topicRepository)
{
    public Either<EgError, Option<TopicEntity>> Execute(
        string reference,
        ExercisesContext dbContext
    )
    {
        if (string.IsNullOrWhiteSpace(reference)
            || string.IsNullOrEmpty(reference))
        {
            return Left(new EgError($"Either name or reference wasn't provided."));
        }

        return from r in topicRepository.FindByReferencePredicate(reference, dbContext)
            select r;
    }
}