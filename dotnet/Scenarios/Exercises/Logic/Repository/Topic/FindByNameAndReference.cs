namespace EncyclopediaGalactica.Scenarios.Exercises.Logic.Repository.Topic;

using Common;
using Models;
using static Prelude;

public partial class TopicRepository
{
    public Either<EgError, Option<TopicEntity>> FindByNameAndReference(
        string name,
        string reference,
        ExercisesContext dbContext
    )
    {
        try
        {
            TopicEntity? result = dbContext.Topics
                .FirstOrDefault(t => t.Name == name && t.Reference == reference);
            if (result == null)
            {
                return Right(Option<TopicEntity>.None);
            }

            return Right(Option<TopicEntity>.Some(result));
        }
        catch (Exception e)
        {
            return Left(
                new EgError($"There is no {nameof(TopicEntity)} with name: {name} and reference: {reference}.")
            );
        }
    }
}