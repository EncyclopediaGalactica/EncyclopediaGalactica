namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Dtos;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public List<StructureDto> MapStructuresToStructureDtos(List<Structure> structures)
    {
        List<StructureDto> result = new List<StructureDto>();
        if (structures.Any())
        {
            foreach (Structure structure in structures)
            {
                result.Add(MapStructureToStructureDto(structure));
            }
        }

        return result;
    }
}