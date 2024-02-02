namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Structure;

using Contracts.Input;

public interface IAddNewStructureCommand
{
    /// <summary>
    ///     Adds a new <see cref="Structure" /> to the system based on the provided information in the
    ///     <see cref="StructureInput" />.
    /// </summary>
    /// <param name="parentId">Id of the parent <see cref="Structure" /></param>
    /// <param name="structureInput">The <see cref="StructureInput" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<StructureInput> AddNewAsync(
        long parentId,
        StructureInput structureInput,
        CancellationToken cancellationToken = default);
}