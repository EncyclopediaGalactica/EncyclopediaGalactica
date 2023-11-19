namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Structure;

using Contracts.Input;

public interface IAddNewStructureCommand
{
    /// <summary>
    ///     Adds a new <see cref="Structure" /> to the system based on the provided information in the
    ///     <see cref="StructureInputContract" />.
    /// </summary>
    /// <param name="parentId">Id of the parent <see cref="Structure" /></param>
    /// <param name="structureInputContract">The <see cref="StructureInputContract" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<StructureInputContract> AddNewAsync(
        long parentId,
        StructureInputContract structureInputContract,
        CancellationToken cancellationToken = default);
}