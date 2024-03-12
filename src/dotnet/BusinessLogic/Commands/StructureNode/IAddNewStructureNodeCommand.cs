namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;

/// <summary>
///     Add a New Structure Node to the system.
/// </summary>
public interface IAddNewStructureNodeCommand
{
    /// <summary>
    ///     Creates a new <see cref="StructureNode" /> to the system based on the provided information in the
    ///     <see cref="StructureNodeInput" />.
    /// </summary>
    /// <param name="structureNodeInput">The <see cref="StructureNodeInput" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task AddNewAsync(StructureNodeInput structureNodeInput, CancellationToken cancellationToken = default);
}