namespace EncyclopediaGalactica.Common.Contracts;

/// <summary>
///
/// </summary>
public class RelationResult
{
    public long Id { get; set; }
    public DocumentResult LeftDocument { get; set; } = new DocumentResult();
    public long LeftDocumentId { get; set; }
    public DocumentResult RightDocument { get; set; } = new DocumentResult();
    public long RightDocumentId { get; set; }
    public RelationTypeResult RelationType { get; set; } = new RelationTypeResult();
    public long RelationTypeId { get; set; }
}