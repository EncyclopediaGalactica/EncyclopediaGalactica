namespace EncyclopediaGalactica.Services.Document.Dtos;

public class StructureDto
{
    public long Id { get; set; }
    public StructureDto Parent { get; set; }
    public List<StructureDto> Children { get; set; } = new List<StructureDto>();
    public long ParentId { get; set; }
}