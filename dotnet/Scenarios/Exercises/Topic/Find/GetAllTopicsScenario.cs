namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Find;

using Common;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Topic;

public class GetAllTopicsScenario(
    TopicRepository repository
)
{
    public Either<EgError, List<TopicEntity>> Execute(
        ExercisesContext ctx
    ) =>
        from all in repository.FindAll(ctx)
        select all;
}