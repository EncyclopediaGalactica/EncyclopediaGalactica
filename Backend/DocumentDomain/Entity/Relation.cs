namespace EncyclopediaGalactica.DocumentDomain.Entity;

/// <summary>
/// </summary>
public class Relation
{
    public long Id { get; set; }
    public long LeftId { get; set; }
    public long RightId { get; set; }
}