namespace EncyclopediaGalactica.Services.Document.Scenario.Interfaces.StructureNode;

using Contracts.Input;
using Contracts.Output;
using Entities;

public interface IAddNewStructureScenario
{
    /// <summary>
    ///     Creates a new <see cref="StructureNode" /> to the system based on the provided information in the
    ///     <see cref="StructureNodeInput" />.
    /// </summary>
    /// <param name="parentId">Id of the parent <see cref="Structure" /></param>
    /// <param name="structureNodeInput">The <see cref="StructureNodeInput" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<StructureNodeResult> AddNewAsync(
        long parentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default);
}