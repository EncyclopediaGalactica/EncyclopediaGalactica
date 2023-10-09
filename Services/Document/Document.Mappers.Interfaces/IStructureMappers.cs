namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Dtos;
using Entities;

/// <summary>
///     IStructure mappers
///     <remarks>
///         Interface for mapping between <see cref="Structure" /> and <see cref="StructureDto" />.
///     </remarks>
/// </summary>
public interface IStructureMappers
{
    /// <summary>
    ///     Maps <see cref="StructureDto" /> object to <see cref="Structure" />.
    /// </summary>
    /// <param name="structureDto">
    ///     <see cref="StructureDto" />
    /// </param>
    /// <returns>Mapped <see cref="Structure" /> object.</returns>
    Structure MapStructureDtoToStructure(StructureDto structureDto);

    /// <summary>
    ///     Maps <see cref="StructureDto" /> objects to <see cref="Structure" /> objects.
    /// </summary>
    /// <param name="structureDtos">List of <see cref="StructureDto" />s.</param>
    /// <returns>List of <see cref="Structure" /> objects.</returns>
    List<Structure> MapStructureDtosToStructures(List<StructureDto> structureDtos);

    /// <summary>
    ///     Maps <see cref="Structure" /> object to <see cref="StructureDto" /> object.
    /// </summary>
    /// <param name="s"><see cref="Structure" />.</param>
    /// <returns>Mapped <see cref="StructureDto" /> object.</returns>
    StructureDto MapStructureToStructureDto(Structure s);

    /// <summary>
    ///     Maps list of <see cref="Structure" /> objects to list of <see cref="StructureDto" /> objects.
    /// </summary>
    /// <param name="structures">List of <see cref="Structure" /> objects.</param>
    /// <returns>List of mapped <see cref="StructureDto" /> objects.</returns>
    List<StructureDto> MapStructuresToStructureDtos(List<Structure> structures);
}