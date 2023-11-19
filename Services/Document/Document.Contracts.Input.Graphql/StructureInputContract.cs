namespace EncyclopediaGalactica.Services.Document.Contracts.Input;

public class StructureInputContract
{
    public long Id { get; set; }
    public StructureInputContract Parent { get; set; }
    public List<StructureInputContract> Children { get; set; } = new List<StructureInputContract>();
    public long ParentId { get; set; }
}