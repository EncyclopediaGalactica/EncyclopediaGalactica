namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Contracts.Input;
using Entities;

/// <summary>
///     IStructure mappers
///     <remarks>
///         Interface for mapping between <see cref="Structure" /> and <see cref="StructureInputContract" />.
///     </remarks>
/// </summary>
public interface IStructureMappers
{
    /// <summary>
    ///     Maps <see cref="StructureInputContract" /> object to <see cref="Structure" />.
    /// </summary>
    /// <param name="structureInputContract">
    ///     <see cref="StructureInputContract" />
    /// </param>
    /// <returns>Mapped <see cref="Structure" /> object.</returns>
    Structure MapStructureDtoToStructure(StructureInputContract structureInputContract);

    /// <summary>
    ///     Maps <see cref="StructureInputContract" /> objects to <see cref="Structure" /> objects.
    /// </summary>
    /// <param name="structureDtos">List of <see cref="StructureInputContract" />s.</param>
    /// <returns>List of <see cref="Structure" /> objects.</returns>
    List<Structure> MapStructureDtosToStructures(List<StructureInputContract> structureDtos);

    /// <summary>
    ///     Maps <see cref="Structure" /> object to <see cref="StructureInputContract" /> object.
    /// </summary>
    /// <param name="s"><see cref="Structure" />.</param>
    /// <returns>Mapped <see cref="StructureInputContract" /> object.</returns>
    StructureInputContract MapStructureToStructureDto(Structure s);

    /// <summary>
    ///     Maps list of <see cref="Structure" /> objects to list of <see cref="StructureInputContract" /> objects.
    /// </summary>
    /// <param name="structures">List of <see cref="Structure" /> objects.</param>
    /// <returns>List of mapped <see cref="StructureInputContract" /> objects.</returns>
    List<StructureInputContract> MapStructuresToStructureDtos(List<Structure> structures);
}