namespace EncyclopediaGalactica.BusinessLogic.Contracts;

public class RelationResult
{
    public long Id { get; set; }
    public DocumentResult LeftDocument { get; set; }
    public DocumentResult RightDocument { get; set; }
    public RelationTypeResult RelationType { get; set; }
}
