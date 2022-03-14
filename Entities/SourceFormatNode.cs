namespace EncyclopediaGalactica.SourceFormats.Worker.Entities;

public class SourceFormatNode
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public int IsRootNode { get; set; }
    public long? ParentNodeId { get; set; }
    public long? RootNodeId { get; set; }
    public List<SourceFormatNode> ChildrenSourceFormatNodes { get; set; } = new List<SourceFormatNode>();
    public SourceFormatNode? ParentSourceFormatNode { get; set; }

    public SourceFormatNode()
    {
    }

    public SourceFormatNode(string? name)
    {
        Name = name;
    }
}