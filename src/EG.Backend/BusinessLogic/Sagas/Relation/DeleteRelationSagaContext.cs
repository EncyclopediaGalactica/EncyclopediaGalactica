namespace EncyclopediaGalactica.BusinessLogic.Sagas.Relation;

using Interfaces;

public class DeleteRelationSagaContext : ISagaContextWithPayload<long>
{
    public long Payload { get; set; }
}