namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Add;

using Common;
using Logic.CatalogParser.Model;
using Logic.Repository;
using Logic.Repository.Models;
using Logic.Repository.Topic;

public class AddNewTopicScenario(
    AddNewTopicScenarioInputValidator validator,
    TopicRepository topicRepository
)
{
    public Either<EgError, TopicEntity> Execute(Topic parsedTopic, ExercisesContext ctx) =>
        from mappedInput in MapInputToEntity(parsedTopic)
        from validatedInput in ValidateInput(mappedInput)
        from newTopic in SaveNewTopic(validatedInput, ctx)
        select newTopic;

    private Either<EgError, TopicEntity> SaveNewTopic(TopicEntity input, ExercisesContext ctx) =>
        topicRepository.AddNewTopic(input, ctx);

    private Either<EgError, TopicEntity> MapInputToEntity(Topic parsedTopic) =>
        parsedTopic.ToTopicEntity();

    private Either<EgError, TopicEntity> ValidateInput(TopicEntity topicEntity) =>
        validator.IsValid(topicEntity);
}