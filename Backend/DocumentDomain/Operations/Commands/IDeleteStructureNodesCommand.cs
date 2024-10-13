namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

public interface IDeleteStructureNodesCommand
{
    /// <summary>
    ///     Deletes all <see cref="StructureNode" /> from the system belongs to the designated <see cref="Document" />.
    /// </summary>
    /// <param name="documentId">The <see cref="Document" /> id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns></returns>
    Task DeleteAsync(long documentId, CancellationToken cancellationToken = default);
}