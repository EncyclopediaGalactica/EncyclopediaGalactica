namespace EncyclopediaGalactica.BusinessLogic.Sagas.Relation;

using Contracts;
using Interfaces;

public class NewRelationSagaContext : ISagaContextWithPayload<RelationInput>
{
    public RelationInput Payload { get; set; }
}