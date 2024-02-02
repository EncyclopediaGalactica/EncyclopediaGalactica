namespace EncyclopediaGalactica.Services.Document.Contracts.Input;

public class StructureInput
{
    public long Id { get; set; }
    public StructureInput Parent { get; set; }
    public List<StructureInput> Children { get; set; } = new List<StructureInput>();
    public long ParentId { get; set; }
}