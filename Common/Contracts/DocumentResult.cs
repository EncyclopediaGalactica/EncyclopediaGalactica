namespace EncyclopediaGalactica.Common.Contracts;

public class DocumentResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Uri? Uri { get; set; }

    public DocumentStructureResult DocumentStructure { get; set; }
}