namespace EncyclopediaGalactica.BusinessLogic.Contracts;

public class StructureNodeInput
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public int IsRootNode { get; set; }
}