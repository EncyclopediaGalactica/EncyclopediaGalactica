namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public List<StructureInputContract> MapStructuresToStructureDtos(List<Structure> structures)
    {
        List<StructureInputContract> result = new List<StructureInputContract>();
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