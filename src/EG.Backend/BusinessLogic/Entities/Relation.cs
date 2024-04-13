namespace EncyclopediaGalactica.BusinessLogic.Entities;

public class Relation
{
    public long Id { get; set; }
    public long LeftEndStructureNodeId { get; set; }
    public long RightEndStructureNodeId { get; set; }
}