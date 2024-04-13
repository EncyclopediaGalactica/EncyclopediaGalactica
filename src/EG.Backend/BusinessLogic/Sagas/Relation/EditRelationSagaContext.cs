namespace EncyclopediaGalactica.BusinessLogic.Sagas.Relation;

using Contracts;
using Interfaces;

public class EditRelationSagaContext : ISagaContextWithPayload<RelationInput>
{
    public RelationInput Payload { get; set; }
}