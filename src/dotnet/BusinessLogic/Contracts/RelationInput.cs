namespace EncyclopediaGalactica.BusinessLogic.Contracts;

public class RelationInput
{
    public long Id { get; set; }
    public long LeftEndId { get; set; }
    public long RightEndId { get; set; }
}