namespace EncyclopediaGalactica.Services.Document.Scenario.Interfaces.StructureNode;

using Contracts.Input;
using Contracts.Output;

public interface IAddNewRootStructureNodeScenario
{
    Task<StructureNodeResult> AddNewRootNodeAsync(
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default);
}