namespace UIWasm.Components.Modules.Documents.DocumentStructureEditor;

public class StructureNode
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public long ParentId { get; set; }
    public List<StructureNode> StructureNodes { get; set; }
}