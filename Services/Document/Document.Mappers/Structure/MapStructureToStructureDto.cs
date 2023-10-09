namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Dtos;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public StructureDto MapStructureToStructureDto(Structure s)
    {
        return new StructureDto
        {
            Id = s.Id,
            ParentId = s.ParentId,
            Children = MapStructuresToStructureDtos(s.Children)
        };
    }
}