namespace EncyclopediaGalactica.BusinessLogic.Sagas.Relation;

using Interfaces;

public class GetRelationByIdSagaContext : ISagaContextWithPayload<long>
{
    public long Payload { get; set; }
}