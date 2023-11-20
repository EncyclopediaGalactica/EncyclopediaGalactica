namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Contracts.Input;
using Entities;

/// <summary>
///     IStructure mappers
///     <remarks>
///         Interface for mapping between <see cref="Structure" /> and <see cref="StructureInput" />.
///     </remarks>
/// </summary>
public interface IStructureMappers
{
    /// <summary>
    ///     Maps <see cref="StructureInput" /> object to <see cref="Structure" />.
    /// </summary>
    /// <param name="structureInput">
    ///     <see cref="StructureInput" />
    /// </param>
    /// <returns>Mapped <see cref="Structure" /> object.</returns>
    Structure MapStructureDtoToStructure(StructureInput structureInput);

    /// <summary>
    ///     Maps <see cref="StructureInput" /> objects to <see cref="Structure" /> objects.
    /// </summary>
    /// <param name="structureDtos">List of <see cref="StructureInput" />s.</param>
    /// <returns>List of <see cref="Structure" /> objects.</returns>
    List<Structure> MapStructureDtosToStructures(List<StructureInput> structureDtos);

    /// <summary>
    ///     Maps <see cref="Structure" /> object to <see cref="StructureInput" /> object.
    /// </summary>
    /// <param name="s"><see cref="Structure" />.</param>
    /// <returns>Mapped <see cref="StructureInput" /> object.</returns>
    StructureInput MapStructureToStructureDto(Structure s);

    /// <summary>
    ///     Maps list of <see cref="Structure" /> objects to list of <see cref="StructureInput" /> objects.
    /// </summary>
    /// <param name="structures">List of <see cref="Structure" /> objects.</param>
    /// <returns>List of mapped <see cref="StructureInput" /> objects.</returns>
    List<StructureInput> MapStructuresToStructureDtos(List<Structure> structures);
}