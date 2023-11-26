namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public List<StructureInput> MapStructuresToStructureDtos(List<Structure> structures)
    {
        List<StructureInput> result = new List<StructureInput>();
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