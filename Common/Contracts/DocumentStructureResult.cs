namespace EncyclopediaGalactica.Common.Contracts;

public class DocumentStructureResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DocumentStructureNodeResult StructureNode { get; set; }
}