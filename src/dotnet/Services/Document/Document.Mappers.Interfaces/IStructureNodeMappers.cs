namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Contracts.Input;
using Contracts.Output;
using Entities;

/// <summary>
///     IStructure mappers
///     <remarks>
///         Interface for mapping between <see cref="StructureNode" /> and <see cref="StructureNodeInput" />.
///     </remarks>
/// </summary>
public interface IStructureNodeMappers
{
    /// <summary>
    ///     Maps <see cref="StructureNodeInput" /> object to <see cref="StructureNode" />.
    /// </summary>
    /// <param name="structureNodeInput">
    ///     <see cref="StructureNodeInput" />
    /// </param>
    /// <returns>Mapped <see cref="StructureNode" /> object.</returns>
    StructureNode MapStructureNodeInputToStructureNode(StructureNodeInput structureNodeInput);

    /// <summary>
    ///     Maps <see cref="StructureNode" /> object to <see cref="StructureNodeInput" /> object.
    /// </summary>
    /// <param name="s"><see cref="StructureNode" />.</param>
    /// <returns>Mapped <see cref="StructureNodeInput" /> object.</returns>
    StructureNodeResult MapStructureNodeToStructureNodeResult(StructureNode s);
}