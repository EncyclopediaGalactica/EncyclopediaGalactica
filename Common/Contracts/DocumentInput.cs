namespace EncyclopediaGalactica.Common.Contracts;

public class DocumentInput
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Uri? Uri { get; set; }

    public DocumentStructureNodeInput? RootStructureNode { get; set; }
}