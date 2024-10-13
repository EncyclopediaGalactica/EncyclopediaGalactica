namespace EncyclopediaGalactica.Common.Contracts;

public class RelationTypeResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<DocumentGroupResult> DocumentGroups { get; set; }
}