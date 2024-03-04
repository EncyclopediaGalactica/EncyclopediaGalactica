namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Structure;

using Contracts.Output;

public interface IGetStructureNodeScenario
{
    StructureNodeResult GetNode(long id);
}