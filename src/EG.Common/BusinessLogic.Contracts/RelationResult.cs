namespace EncyclopediaGalactica.BusinessLogic.Contracts;

public class RelationResult
{
    public long Id { get; set; }
    public DocumentResult LeftDocument { get; set; }
    public long LeftDocumentId { get; set; }
    public DocumentResult RightDocument { get; set; }
    public long RightDocumentId { get; set; }
    public RelationTypeResult RelationType { get; set; }
}
