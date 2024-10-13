namespace EncyclopediaGalactica.Common.Contracts;

public class DocumentStructureNodeResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long DocumentId { get; set; }
    public long ParentId { get; set; }
    public bool HasChildren { get; set; }
    public ICollection<DocumentStructureNodeResult> StructureNodes { get; set; } = new List<DocumentStructureNodeResult>();
}