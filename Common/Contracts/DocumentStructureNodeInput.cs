namespace EncyclopediaGalactica.Common.Contracts;

public class DocumentStructureNodeInput
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public int IsRootNode { get; set; }
}