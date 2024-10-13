namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

/// <summary>
///     IStructure mappers
///     <remarks>
///         Interface for mapping between <see cref="DocumentStructureNode" /> and <see cref="StructureNodeInput" />.
///     </remarks>
/// </summary>
public interface IDocumentStructureNodeMapper
{
    /// <summary>
    ///     Maps <see cref="StructureNodeInput" /> object to <see cref="DocumentStructureNode" />.
    /// </summary>
    /// <param name="structureNodeInput">
    ///     <see cref="StructureNodeInput" />
    /// </param>
    /// <returns>Mapped <see cref="DocumentStructureNode" /> object.</returns>
    DocumentStructureNode MapStructureNodeInputToStructureNode(DocumentStructureNodeInput structureNodeInput);

    /// <summary>
    ///     Maps <see cref="DocumentStructureNode" /> object to <see cref="StructureNodeInput" /> object.
    /// </summary>
    /// <param name="s"><see cref="DocumentStructureNode" />.</param>
    /// <returns>Mapped <see cref="StructureNodeInput" /> object.</returns>
    DocumentStructureNodeInput MapStructureNodeToStructureNodeResult(DocumentStructureNode s);
}