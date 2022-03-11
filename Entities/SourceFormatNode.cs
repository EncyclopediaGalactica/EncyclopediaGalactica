namespace EncyclopediaGalactica.SourceFormats.Worker.Entities;

using System.Collections.ObjectModel;

public class SourceFormatNode
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public int IsRootNode { get; set; }
    public long? ParentNodeId { get; set; }
    public long? RootNodeId { get; set; }
    public Collection<SourceFormatNode> ChildrenSourceFormatNodes { get; } = new Collection<SourceFormatNode>();
    public SourceFormatNode? ParentSourceFormatNode { get; set; }

    public SourceFormatNode()
    {
    }

    public SourceFormatNode(string? name)
    {
        Name = name;
    }
}